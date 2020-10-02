using SystemBase.StateMachineBase;
using Systems;
using Systems.GameState.Messages;
using UniRx;

namespace GameState.States
{
    [NextValidStates(typeof(StartScreen))]
    public class GameOver : BaseState<Game>
    {
        public override void Enter(StateContext<Game> context)
        {
            MessageBroker.Default.Receive<GameMsgRestart>()
                .Subscribe(restart => context.GoToState(new StartScreen()))
                .AddTo(this);
        }
    }
}