using SystemBase;
using UniRx;

namespace Assets.Systems.Movement
{
    [GameSystem]
    public class MovementSystem : GameSystem<MovementComponent>
    {
        public override void Register(MovementComponent comp)
        {
            SystemUpdate(comp)
                .Subscribe(UpdateMutators)
                .AddTo(comp);
        }

        private void UpdateMutators(MovementComponent comp)
        {
            var direction = comp.Direction.Value;
            var speed = comp.Speed.Value;

            foreach (var mutator in comp.MovementMutators)
            {
                mutator.Mutate(comp.CanMove.Value, direction, speed, out direction, out speed);
            }

            comp.CurrentVelocity.Value = direction * speed;
            comp.transform.position += direction * speed;
        }
    }
}
