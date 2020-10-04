using System;
using SystemBase;
using Assets.GameState.Messages;
using Assets.Systems.Obsticles;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.NeedleCollision
{
    [GameSystem]
    public class NeedleCollisionSystem : GameSystem<NeedleCollisionComponent>
    {
        private const float DustSlowAmount = .2f;
        private const double DustSlowLength = .5f;

        public override void Register(NeedleCollisionComponent comp)
        {
            comp.OnTriggerEnterAsObservable().Subscribe(OnTriggerEnter).AddTo(comp);
        }

        private static void OnTriggerEnter(Collider collider)
        {
            var dustComponent = collider.gameObject.GetComponent<DustComponent>();
            if (dustComponent != null)
            {
                Observable.Timer(TimeSpan.FromSeconds(DustSlowLength)).Subscribe(_ => EndSlowEffect()).AddTo(dustComponent);
                dustComponent.Jump.Execute();
                MessageBroker.Default.Publish(new NeedleChangeSpeedMsg(-DustSlowAmount));
            }

            //MessageBroker.Default.Publish(new VinylJumpMsg(.02f));
        }

        private static void EndSlowEffect()
        {
            MessageBroker.Default.Publish(new NeedleChangeSpeedMsg(DustSlowAmount));
        }
    }
}
