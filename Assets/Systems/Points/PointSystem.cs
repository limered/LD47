using System;
using SystemBase;
using Assets.Systems.VinylMusicSystem;
using GameState.States;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Assets.Systems.Points
{
    [GameSystem]
    public class PointSystem : GameSystem<PointComponent, VinylMusicComponent>
    {
        private DateTime _startTime;

        public override void Register(PointComponent component)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => SetStartTime(component))
                .AddTo(component);

            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusicComponent => UpdatePoints(component, vinylMusicComponent))
                .AddTo(component);
        }

        private void SetStartTime(Component component)
        {
            component.GetComponent<Text>().text = "00:00:0";
            _startTime = DateTime.Now;
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private void UpdatePoints(PointComponent comp, VinylMusicComponent vinylMusicComponent)
        {
            if (!(IoC.Game.GameStateContext.CurrentState.Value is Running)) return;

            var actualPassedTime = DateTime.Now - _startTime;
            //var passedMusicTime = vinylMusicComponent.VinylMusicSource.time;

            //var passedTimeDiff = actualPassedTime - passedMusicTime;
            //var passedTimeDiffTimeSpan = TimeSpan.FromSeconds(passedTimeDiff);
            var formattedTimeDiff =
                $"{actualPassedTime.Minutes:D2}:{actualPassedTime.Seconds:D2}:{actualPassedTime.Milliseconds / 100}";

            var textComponent = comp.GetComponent<Text>();
            textComponent.text = formattedTimeDiff;
        }
    }
}
