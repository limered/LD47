using System;
using System.Collections.ObjectModel;

namespace SystemBase.StateMachineBase
{
    public interface IState<T>
    {
        ReadOnlyCollection<Type> ValidNextStates { get; }
        void Enter(IStateContext<IState<T>, T> context);
        bool Exit();
    }
}
