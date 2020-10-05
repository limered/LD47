using System.Net.Http.Headers;
using Assets.Systems.Tools.Actions;
using Assets.Systems.UI;
using Assets.Systems.VinylMusicSystem.Events;
using GameState.States;
using SystemBase;
using Systems.GameState.Messages;
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

            MessageBroker.Default.Receive<PlayButtonClickedEvent>()
                .Subscribe(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked(PlayButtonClickedEvent obj)
        {
            if (IoC.Game.GameStateContext.CurrentState.Value is GameOver)
            {
                MessageBroker.Default.Publish(new GameMsgRestart());
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
        }
    }
}
