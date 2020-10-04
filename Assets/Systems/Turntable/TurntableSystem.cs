using SystemBase;
using Assets.Systems.VinylMusicSystem;
using GameState.States;
using UniRx;
using UnityEngine;
using Utils;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm, VinylMusicComponent>
    {
        public override void Register(Turntable component)
        {
            RegisterWaitable(component);

            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => RegisterTurntable(component))
                .AddTo(component);
        }

        public override void Register(Vinyl component)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => RegisterVinyl(component))
                .AddTo(component);
        }

        public override void Register(Arm component)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => RegisterArm(component))
                .AddTo(component);
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private void RegisterTurntable(Turntable component)
        {
            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusic => ChangeRotationSpeed(vinylMusic, component))
                .AddTo(component);
        }

        private void RegisterVinyl(Vinyl component)
        {
            WaitOn<Turntable>()
                .ThenOnUpdate(turntable => component
                    .transform
                    .Rotate(component.Axis, Time.deltaTime * -turntable.Speed.Value, Space.Self))
                .AddTo(component);

            StartVinylAnimation(component);
        }

        private static void StartVinylAnimation(Vinyl component)
        {
            component.VinylAnimationGameObject.GetComponent<Animator>().enabled = true;
        }

        private void RegisterArm(Arm component)
        {
            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusicComponent => UpdateNeedlePosition(component, vinylMusicComponent))
                .AddTo(component);
        }

        private void ChangeRotationSpeed(VinylMusicComponent vinylMusic, Turntable component)
        {
            component.Speed.Value = component.MaxSpeed * vinylMusic.VinylMusicSource.pitch;
        }

        private void UpdateNeedlePosition(Arm component, VinylMusicComponent vinylMusicComponent)
        {
            var rotationDifference = component.EndRotation - component.StartRotation;
            var absoluteDifference = rotationDifference * vinylMusicComponent.MusicProgress.Value;
            var currentRotation = component.StartRotation + absoluteDifference;

            component.transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        }
    }
}
