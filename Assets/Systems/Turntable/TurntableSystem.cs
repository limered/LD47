using Assets.Systems.VinylMusicSystem;
using GameState.States;
using SystemBase;
using Systems.GameState.Messages;
using Assets.Systems.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm, VinylMusicComponent>
    {
        private bool _rotatingToStartPosition;

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

            MessageBroker.Default.Receive<PlayButtonClickedEvent>().Subscribe(_ => PlayButtonPressed(component))
                .AddTo(component);
        }

        public override void Register(VinylMusicComponent component) => RegisterWaitable(component);

        private void PlayButtonPressed(Arm comp)
        {
            var startRotation = comp.transform.eulerAngles;
            var endRotation = startRotation;
            endRotation.z = comp.StartRotation;
            _rotatingToStartPosition = true;

            comp.UpdateAsObservable().Subscribe(_ => RotateToStartPosition(comp.transform, endRotation)).AddTo(comp);
        }

        private void RotateToStartPosition(Transform t, Vector3 endRotation)
        {
            if (!_rotatingToStartPosition)
            {
                return;
            }

            if (_rotatingToStartPosition && Vector3.Distance(t.eulerAngles, endRotation) > 0.05f)
            {
                t.eulerAngles = Vector3.Lerp(t.rotation.eulerAngles, endRotation, Time.deltaTime * 2);
            }
            else
            {
                _rotatingToStartPosition = false;
                MessageBroker.Default.Publish(new TurntableReady());
            }
        }

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
