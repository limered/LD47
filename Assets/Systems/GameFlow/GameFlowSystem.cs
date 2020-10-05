using Assets.Systems.Tools.Actions;
using Assets.Systems.VinylMusicSystem.Events;
using GameState.States;
using SystemBase;
using Systems.GameState.Messages;
using Assets.Systems.Turntable;
using UniRx;
using Utils;

namespace Assets.Systems.GameFlow
{
    [GameSystem]
    public class GameFlowSystem : GameSystem
    {
        public override void Init()
        {
            MessageBroker.Default.Receive<MusicEndedEvent>()
                .Subscribe(OnMusicEnd);

            MessageBroker.Default.Receive<TurntableReady>()
                .Subscribe(OnTurntableReady);
        }

        private void OnTurntableReady(TurntableReady obj)
        {
            if (IoC.Game.GameStateContext.CurrentState.Value is GameOver)
            {
                MessageBroker.Default.Publish(new GameMsgStart());
            }
            else
            {
                MessageBroker.Default.Publish(new GameMsgStart());
            }
            
            MessageBroker.Default.Publish(new SelectToolAction { ToolToSelect = CurrentTool.Broom });
        }

        private void OnMusicEnd(MusicEndedEvent obj)
        {
            MessageBroker.Default.Publish(new GameMsgEnd());
            MessageBroker.Default.Publish(new SelectToolAction { ToolToSelect = CurrentTool.Broom });
        }
    }
}
