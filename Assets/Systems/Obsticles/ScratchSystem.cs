using Assets.Systems.VinylMusicSystem;
using SystemBase;
using UniRx;
using UnityEngine;

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
        }

        public override void Register(ScratchConfigComponent component)
        {
            RegisterWaitable(component);
            _scrathConfig = component;
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
