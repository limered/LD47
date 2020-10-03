using SystemBase;
using UniRx;
using UniRx.Triggers;

namespace Assets.Systems.Movement
{
    [GameSystem]
    public class MovementSystem : GameSystem<MovementComponent>
    {
        public override void Register(MovementComponent comp)
        {
            comp.UpdateAsObservable()
            .Subscribe(UpdateMutators(comp))
            .AddTo(comp);
        }

        private static System.Action<Unit> UpdateMutators(MovementComponent comp)
        {
            return _ =>
            {
                var direction = comp.Direction.Value;
                var speed = comp.Speed.Value;

                foreach (var mutator in comp.MovementMutators)
                {
                    mutator.Mutate(comp.CanMove.Value, direction, speed, out direction, out speed);
                }
                comp.transform.position += direction * speed;
            };
        }
    }
}