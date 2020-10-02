using System;
using System.Collections.Generic;
using UniRx;
using Systems;

namespace Utils
{
    public static class IoC
    {
        public static Game Game => Resolve<Game>();

        private static readonly Dictionary<Type, object> Singletons = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, Subject<Unit>> SingletonPromises = new Dictionary<Type, Subject<Unit>>();

        public static TSingleton Resolve<TSingleton>()
        {
            var found = false;

            if (Singletons.ContainsKey(typeof(TSingleton)) && Singletons[typeof(TSingleton)] is Func<TSingleton>)
            {
                Singletons[typeof(TSingleton)] = ((Func<TSingleton>)Singletons[typeof(TSingleton)])();
                found = true;
            }
            if (found || Singletons.ContainsKey(typeof(TSingleton)))
                return (TSingleton)Singletons[typeof(TSingleton)];
            throw new KeyNotFoundException("unknown interface: " + typeof(TSingleton).FullName);
        }

        private static Subject<Unit> GetSingletonSubject<TSingleton>()
        {
            if (!SingletonPromises.ContainsKey(typeof(TSingleton)))
            {
                SingletonPromises.Add(typeof(TSingleton), new Subject<Unit>());
            }
            return SingletonPromises[typeof(TSingleton)];
        }

        public static IObservable<TSingleton> OnResolve<TSingleton>()
        {
            var sub = GetSingletonSubject<TSingleton>();

            // ReSharper disable once InvertIf
            if (Singletons.ContainsKey(typeof(TSingleton)) && Singletons[typeof(TSingleton)] is Func<TSingleton>)
            {
                Singletons[typeof(TSingleton)] = ((Func<TSingleton>)Singletons[typeof(TSingleton)])();
                SingletonPromises[typeof(TSingleton)] = sub = null;
            }

            return sub == null ? Observable.Return(Resolve<TSingleton>()) : sub.Select(_ => Resolve<TSingleton>());
        }

        public static IObservable<TNext> OnResolve<TSingleton, TNext>(Func<TSingleton, IObservable<TNext>> andThen)
        {
            return OnResolve<TSingleton>().ContinueWith(andThen);
        }

        public static TSingleton ResolveOrDefault<TSingleton>()
        {
            var found = false;

            if (Singletons.ContainsKey(typeof(TSingleton)) && Singletons[typeof(TSingleton)] is Func<TSingleton>)
            {
                Singletons[typeof(TSingleton)] = ((Func<TSingleton>)Singletons[typeof(TSingleton)])();
                found = true;
            }
            if (found || Singletons.ContainsKey(typeof(TSingleton)))
                return (TSingleton)Singletons[typeof(TSingleton)];
            return default(TSingleton);
        }

        public static void RegisterSingleton<TSingleton>(TSingleton singletonObject)
        {
            if (Singletons.ContainsKey(typeof(TSingleton)))
            {
                Singletons[typeof(TSingleton)] = singletonObject;
            }
            else
            {
                Singletons.Add(typeof(TSingleton), singletonObject);
            }

            var sub = GetSingletonSubject<TSingleton>();
            if (sub == null) return;

            sub.OnNext(Unit.Default);
            sub.OnCompleted();
            sub.Dispose();
            SingletonPromises[typeof(TSingleton)] = null;
        }

        public static void RegisterSingleton<TSingleton>(Func<TSingleton> lazyConstructor)
        {
            if (Singletons.ContainsKey(typeof(TSingleton)))
            {
                Singletons[typeof(TSingleton)] = lazyConstructor;
            }
            else
            {
                Singletons.Add(typeof(TSingleton), lazyConstructor);
            }
        }
    }
}