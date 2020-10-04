using Assets.Systems.VinylMusicSystem;
using GameState.States;
using SystemBase;
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

            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusic => ChangeRotationSpeed(vinylMusic, component))
                .AddTo(component);
        }

        public override void Register(Vinyl component)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => RegisterVinyl(component))
                .AddTo(component);

            WaitOn<Turntable>()
                .ThenOnUpdate(turntable => UpdateSpeed(component, turntable))
                .AddTo(component);
        }

        public override void Register(Arm component)
        {
            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusicComponent => UpdateNeedlePosition(component, vinylMusicComponent))
                .AddTo(component);
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private static void UpdateSpeed(Vinyl component, Turntable turntable)
        {
            if (!(IoC.Game.GameStateContext.CurrentState.Value is Running)) return;

            component.transform
                .Rotate(component.Axis, Time.deltaTime * -turntable.Speed.Value, Space.Self);
        }
        private static void StartVinylAnimation(Vinyl component)
        {
            component.VinylAnimationGameObject.GetComponent<Animator>().enabled = true;
        }

        private void RegisterVinyl(Vinyl component)
        {
            StartVinylAnimation(component);
        }
        private void ChangeRotationSpeed(VinylMusicComponent vinylMusic, Turntable component)
        {
            if (!(IoC.Game.GameStateContext.CurrentState.Value is Running)) return;

            component.Speed.Value = component.MaxSpeed * vinylMusic.VinylMusicSource.pitch;
        }

        private void UpdateNeedlePosition(Arm component, VinylMusicComponent vinylMusicComponent)
        {
            if (!(IoC.Game.GameStateContext.CurrentState.Value is Running)) return;

            var rotationDifference = component.EndRotation - component.StartRotation;
            var absoluteDifference = rotationDifference * vinylMusicComponent.MusicProgress.Value;
            var currentRotation = component.StartRotation + absoluteDifference;

            component.transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        }
    }
}
