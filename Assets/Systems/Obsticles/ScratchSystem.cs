using System;
using Assets.Systems.VinylMusicSystem;
using SystemBase;
using GameState.States;
using UniRx;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Assets.Systems.Obsticles
{
    [GameSystem(typeof(VinylMusicSystem.VinylMusicSystem))]
    public class ScratchSystem : GameSystem<ScratchComponent, ScratchConfigComponent, VinylMusicComponent>
    {
        private ScratchConfigComponent _scrathConfig;

        public override void Register(ScratchComponent component)
        {
            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(musicComponent => AnimateScratch(musicComponent, component))
                .AddTo(component);

            component.Remove.Subscribe(_ => OnRemove(component)).AddTo(component);
        }

        private void OnRemove(ScratchComponent obj)
        {
            obj.StartFadeout();

            Observable
                .Timer(TimeSpan.FromMilliseconds(300))
                .Subscribe(_ => Object.Destroy(obj.gameObject));
        }

        public override void Register(ScratchConfigComponent component)
        {
            RegisterWaitable(component);
            _scrathConfig = component;

            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => ClearAllScratches())
                .AddTo(component);
        }

        private static void ClearAllScratches()
        {
            GameObject[] scratches = GameObject.FindGameObjectsWithTag("Scratch");
            foreach (var scratch in scratches)
            {
                Object.Destroy(scratch);
            }
        }

        public override void Register(VinylMusicComponent component)
        {
            WaitOn<ScratchConfigComponent>()
                .Subscribe(_ => RegisterWaitable(component))
                .AddTo(component);
        }

        private void AnimateScratch(VinylMusicComponent musicComponent, ScratchComponent obj)
        {
            var animationSpeed = Time.realtimeSinceStartup
                                 * musicComponent.VinylMusicSource.pitch
                                 * _scrathConfig.MaxAnimationSpeed;

            var xScaleModifier = Mathf.Sin(animationSpeed) * 0.01f + 0.1f;
            var yScaleModifier = Mathf.Cos(animationSpeed) * 0.01f + 0.1f;

            obj.transform.localScale = new Vector3(xScaleModifier, yScaleModifier, 0.1f);
        }
    }
}
