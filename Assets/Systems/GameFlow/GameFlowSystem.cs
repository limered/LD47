using Assets.Systems.Tools.Actions;
using Assets.Systems.VinylMusicSystem.Events;
using SystemBase;
using Systems.GameState.Messages;
using Assets.Systems.UI;
using UniRx;

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
            MessageBroker.Default.Publish(new GameMsgStart());
            MessageBroker.Default.Publish(new SelectToolAction { ToolToSelect = CurrentTool.Broom });
        }

        private void OnMusicEnd(MusicEndedEvent obj)
        {
            MessageBroker.Default.Publish(new GameMsgEnd());
        }
    }
}
