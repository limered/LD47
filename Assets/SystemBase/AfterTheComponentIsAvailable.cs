using System;
using UniRx;
using UniRx.Triggers;

namespace SystemBase
{
    public class AfterTheComponentIsAvailable<T> : IObservable<T> where T : GameComponent
    {
        readonly IObservable<T> _lazy;

        public AfterTheComponentIsAvailable(IObservable<T> lazy)
        {
            _lazy = lazy;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _lazy.Subscribe(observer);
        }

        public IObservable<U> Then<U>(Func<T, IObservable<U>> then)
        {
            return _lazy.SelectMany(then);
        }

        public IDisposable ThenOnUpdate(Action<T> everyFrame)
        {
            return Then(x => x.UpdateAsObservable().Select(_ => x)).Subscribe(everyFrame);
        }
    }
}