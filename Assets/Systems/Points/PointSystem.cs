using System;
using SystemBase;
using Systems.GameState.Messages;
using Assets.Systems.VinylMusicSystem;
using UniRx;
using UnityEngine.UI;

namespace Assets.Systems.Points
{
    [GameSystem]
    public class PointSystem : GameSystem<PointComponent, VinylMusicComponent>
    {
        private DateTime _startTime;

        public override void Register(PointComponent component)
        {
            MessageBroker.Default.Receive<GameMsgStart>().Subscribe(SetStartTime).AddTo(component);
            WaitOn<VinylMusicComponent>().ThenOnUpdate(vinylMusicComponent => UpdatePoints(component, vinylMusicComponent)).AddTo(component);
        }

        private void SetStartTime(GameMsgStart obj)
        {
            _startTime = DateTime.Now;
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private void UpdatePoints(PointComponent comp, VinylMusicComponent vinylMusicComponent)
        {
            var actualPassedTime = (DateTime.Now - _startTime).TotalSeconds;
            var passedMusicTime = vinylMusicComponent.VinylMusicSource.time;

            var passedTimeDiff = actualPassedTime - passedMusicTime;
            var passedTimeDiffTimeSpan = TimeSpan.FromSeconds(passedTimeDiff);
            var formattedTimeDiff = $"{passedTimeDiffTimeSpan.Minutes:D2}:{passedTimeDiffTimeSpan.Seconds:D2}:{passedTimeDiffTimeSpan.Milliseconds/100}";

            var textComponent = comp.GetComponent<Text>();
            textComponent.text = $"Lost time: {formattedTimeDiff}m";
        }
    }
}
