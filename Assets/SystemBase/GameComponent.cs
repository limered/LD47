using System;
using Systems;
using UniRx;
using UnityEngine;
using Utils;
using Utils.Plugins;

namespace SystemBase
{
    public class GameComponent : MonoBehaviour, IGameComponent
    {
        public virtual IGameSystem System { get; set; }

        public void RegisterToGame()
        {
            IoC.Resolve<Game>().RegisterComponent(this);
        }

        protected void Start()
        {
            RegisterToGame();

            OverwriteStart();
        }

        protected virtual void OverwriteStart() { }

        public IObservable<TComponent> WaitOn<TComponent>(ReactiveProperty<TComponent> componentToWaitOnTo) 
            where TComponent : GameComponent
        {
            return componentToWaitOnTo.WhereNotNull().Select(waitedComponent => waitedComponent);
        }

        public IDisposable WaitOn<TComponent>(ReactiveProperty<TComponent> componentToWaitOnTo, Action<TComponent> onNext)
            where TComponent : GameComponent
        {
            return componentToWaitOnTo
                .WhereNotNull()
                .Select(waitedComponent => waitedComponent)
                .Subscribe(onNext)
                .AddTo(this);
        }
    }

    public class SemanticGameComponent<TGameComponent> : GameComponent where TGameComponent : IGameComponent
    {
        public TGameComponent dependency;
        public TGameComponent Dependency
        {
            get => dependency != null ? dependency : GetComponent<TGameComponent>();
            set => dependency = value;
        }
    }
}
