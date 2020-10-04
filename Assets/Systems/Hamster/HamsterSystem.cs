using SystemBase;
using Assets.Systems.Obsticles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.HamsterCollision
{
    [GameSystem]
    public class HamsterSystem : GameSystem<HamsterComponent, Turntable.Turntable>
    {
        public override void Register(HamsterComponent component)
        {
            component
                .OnTriggerEnterAsObservable()
                .Subscribe(Collider)
                .AddTo(component);

            WaitOn<Turntable.Turntable>()
                .ThenOnUpdate(_ => ResetRotation(component))
                .AddTo(component);
        }

        private void ResetRotation(HamsterComponent comp)
        {
            if (!comp.IsUpright) return;
            comp.transform
                .Rotate(comp.Axis, Time.deltaTime * comp.RotationSpeed, Space.Self);
        }

        private void Collider(Collider other)
        {
            var dust = other.GetComponent<DustComponent>();
            if (dust)
            {
                MoveDustToAnotherPlace(dust);
            }
        }

        private void MoveDustToAnotherPlace(DustComponent dust)
        {
            dust.Jump.Execute();
        }

        public override void Register(Turntable.Turntable component)
        {
            RegisterWaitable(component);
        }

        private void SetAnimationByPosition(HamsterComponent comp)
        {
            var animator = comp.HamsterModel.GetComponent<Animator>();

            var newPosition = comp.RecordModel.transform.position;
            var oldPosition = comp.transform.position;
            if (newPosition.x <= oldPosition.x && newPosition.y >= oldPosition.y)
            {
                animator.Play(Hamster.Move.Up);
            }
            else if (newPosition.x >= oldPosition.x && newPosition.y >= oldPosition.y)
            {
                animator.Play(Hamster.Move.Down);
            }
            else if (newPosition.x >= oldPosition.x && newPosition.y <= oldPosition.y)
            {
                animator.Play(Hamster.Move.Left);
            }
            else if (newPosition.x >= oldPosition.x && newPosition.y >= oldPosition.y)
            {
                animator.Play(Hamster.Move.Right);
            }
        }
    }
}
