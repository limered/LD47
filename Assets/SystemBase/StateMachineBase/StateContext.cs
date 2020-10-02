using UniRx;

namespace SystemBase.StateMachineBase
{
    public class StateContext<T> : IStateContext<BaseState<T>, T>
    {
        public StateContext()
        {
            BeforeStateChange = new ReactiveCommand<BaseState<T>>();
            AfterStateChange = new ReactiveCommand<BaseState<T>>();
        }

        public ReactiveCommand<BaseState<T>> AfterStateChange { get; }

        public ReactiveCommand<BaseState<T>> BeforeStateChange { get; }

        public ReactiveProperty<BaseState<T>> CurrentState { get; private set; }

        public bool GoToState(BaseState<T> state)
        {
            if (!CurrentState.Value.ValidNextStates.Contains(state.GetType()) ||
                !CurrentState.Value.Exit())
            {
                return false;
            }

            BeforeStateChange.Execute(state);

            CurrentState.Value = state;
            CurrentState.Value.Enter(this);

            AfterStateChange.Execute(state);

            return true;
        }

        public void Start(BaseState<T> initialState)
        {
            CurrentState = new ReactiveProperty<BaseState<T>>(initialState);
            CurrentState.Value.Enter(this);
        }
    }
}