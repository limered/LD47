using SystemBase;
using Assets.Systems.VinylMusicSystem;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm, VinylMusicComponent>
    {
        public override void Register(Turntable component) => RegisterWaitable(component);

        public override void Register(Vinyl component)
        {
            WaitOn<Turntable>()
                .ThenOnUpdate(turntable => component
                    .transform
                    .Rotate(component.Axis, Time.deltaTime * -turntable.Speed.Value, Space.Self))
                .AddTo(component);
        }

        public override void Register(Arm component)
        {
            WaitOn<VinylMusicComponent>()
                .ThenOnUpdate(vinylMusicComponent => UpdateNeedlePosition(component, vinylMusicComponent))
                .AddTo(component);
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private void UpdateNeedlePosition(Arm component, VinylMusicComponent vinylMusicComponent)
        {
            var rotationDifference = component.EndRotation - component.StartRotation;
            var absoluteDifference = rotationDifference * vinylMusicComponent.MusicProgress.Value;
            var currentRotation = component.StartRotation + absoluteDifference;

            component.transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        }
    }
}
