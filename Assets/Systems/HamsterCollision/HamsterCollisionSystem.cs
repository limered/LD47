using SystemBase;
using Assets.Systems.Obsticles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.HamsterCollision
{
    [GameSystem]
    public class HamsterCollisionSystem : GameSystem<HamsterComponent>
    {
        public override void Register(HamsterComponent component)
        {
            component
                .OnTriggerEnterAsObservable()
                .Subscribe(Collider)
                .AddTo(component);
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
    }
}
