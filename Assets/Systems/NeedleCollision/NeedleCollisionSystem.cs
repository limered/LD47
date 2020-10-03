using SystemBase;
using Assets.GameState.Messages;
using Assets.Systems.Obsticles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.NeedleCollision
{
    [GameSystem]
    public class NeedleCollisionSystem : GameSystem<NeedleComponent>
    {
        public override void Register(NeedleComponent comp)
        {
            comp.OnTriggerEnterAsObservable().Subscribe(OnTriggerEnter).AddTo(comp);
        }

        private static void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.GetComponent<DustComponent>() != null)
            {
                MessageBroker.Default.Publish(new VinylJumpMsg(.02f));
            }
        }
    }
}
