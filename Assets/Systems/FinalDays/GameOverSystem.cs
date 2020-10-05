using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems;
using GameState.States;
using UniRx;
using UnityEngine;
using Utils;

namespace Assets.Systems
{
    [GameSystem]
    public class GameOverSystem : GameSystem<FinalDiscComponent>
    {
        private FinalDiscComponent _discComponent;

        public override void Register(FinalDiscComponent component)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is GameOver)
                .Subscribe(StartEndSequence)
                .AddTo(component);

            _discComponent = component;
        }

        private void StartEndSequence(BaseState<Game> obj)
        {
            _discComponent.EndMusic.Play();

            _discComponent.StartResize();

            Observable.Timer(TimeSpan.FromSeconds(3.1f))
                .Subscribe(StartColors)
                .AddTo(_discComponent);
        }

        private void StartColors(long obj)
        {
            SystemUpdate()
                .Subscribe(RotateColor)
                .AddTo(_discComponent);
        }

        private void RotateColor(float obj)
        {
            var rendererComp = _discComponent.GetComponentInChildren<SpriteRenderer>();
            Color.RGBToHSV(rendererComp.color, out var h, out var s, out var v);
            rendererComp.color = Color.HSVToRGB(h + 0.005f, 1, v);
        }
    }
}
