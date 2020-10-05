using System;
using SystemBase;
using Assets.GameState.Messages;
using Assets.Systems.HamsterCollision;
using Assets.Systems.Obsticles;
using StrongSystems.Audio;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.NeedleCollision
{
    [GameSystem]
    public class NeedleCollisionSystem : GameSystem<NeedleCollisionComponent, NeedleCollisionConfigComponent>
    {
        public override void Register(NeedleCollisionComponent comp)
        {
            WaitOn<NeedleCollisionConfigComponent>().Subscribe(cfg => SubscribeOnTriggerEnter(cfg, comp)).AddTo(comp);
        }

        private static void SubscribeOnTriggerEnter(NeedleCollisionConfigComponent cfg, NeedleCollisionComponent comp)
        {
            comp.OnTriggerEnterAsObservable().Subscribe(collider => OnTriggerEnter(collider, cfg)).AddTo(comp);
        }

        public override void Register(NeedleCollisionConfigComponent component) => RegisterWaitable(component);

        private static void OnTriggerEnter(Collider collider, NeedleCollisionConfigComponent cfg)
        {
            var dustComponent = collider.gameObject.GetComponent<DustComponent>();
            if (dustComponent != null)
            {
                Observable.Timer(TimeSpan.FromSeconds(cfg.DustSlowLength)).Subscribe(_ => EndSlowEffect(cfg))
                    .AddTo(dustComponent);
                dustComponent.Jump.Execute();
                MessageBroker.Default.Publish(new NeedleChangeSpeedMsg(-cfg.DustSlowAmount));
            }

            var hamsterComponent = collider.gameObject.GetComponent<HamsterComponent>();
            if (hamsterComponent != null)
            {
                "hamster_squeak".Play();
                MessageBroker.Default.Publish(new VinylJumpMsg(1f));
            }

            var scratch = collider.GetComponent<ScratchComponent>();
            if (scratch)
            {
                MessageBroker.Default.Publish(new VinylJumpMsg(cfg.ScratchJumpAmount));
            }
        }

        private static void EndSlowEffect(NeedleCollisionConfigComponent cfg)
        {
            MessageBroker.Default.Publish(new NeedleChangeSpeedMsg(cfg.DustSlowAmount));
        }
    }
}