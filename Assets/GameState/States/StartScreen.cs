using SystemBase.StateMachineBase;
using Systems;
using Systems.GameState.Messages;
using UniRx;

namespace GameState.States
{
    [NextValidStates(typeof(Running))]
    public class StartScreen : BaseState<Game>
    {
        public override void Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgStart>()
                .Subscribe(start => context.GoToState(new Running()))
                .AddTo(this);
        }
    }
}