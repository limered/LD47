using SystemBase;
using Assets.GameState.Messages;
using Assets.Systems.VinylMusicSystem.Events;
using GameState.States;
using StrongSystems.Audio;
using UniRx;
using UnityEngine;
using Utils;

namespace Assets.Systems.VinylMusicSystem
{
    [GameSystem]
    public class VinylMusicSystem : GameSystem<VinylMusicComponent>
    {
        public override void Register(VinylMusicComponent comp)
        {
            IoC.Game.GameStateContext.CurrentState
                .Where(state => state is Running)
                .Subscribe(_ => StartVinylMusic(comp))
                .AddTo(comp);

            MessageBroker.Default.Receive<VinylJumpMsg>().Subscribe(msg => Jump(comp, msg));
            MessageBroker.Default.Receive<NeedleChangeSpeedMsg>().Subscribe(msg => ChangeSpeed(comp, msg));

            SystemUpdate(comp).Subscribe(UpdateMusicProgress).AddTo(comp);
        }

        private void ChangeSpeed(VinylMusicComponent comp, NeedleChangeSpeedMsg msg)
        {
            var newPitch = comp.VinylMusicSource.pitch + msg.SpeedChangeAmount;
            newPitch = Mathf.Clamp(newPitch, -1f, 1f);
            comp.VinylMusicSource.pitch = newPitch;
        }

        private static void UpdateMusicProgress(VinylMusicComponent comp)
        {
            comp.MusicProgress.Value = comp.VinylMusicSource.time / comp.VinylMusicSource.clip.length;

            if (!comp.VinylMusicSource.isPlaying && IoC.Game.GameStateContext.CurrentState.Value is Running)
            {
                MessageBroker.Default.Publish(new MusicEndedEvent());
            }
        }

        private static void StartVinylMusic(VinylMusicComponent comp)
        {
            comp.VinylMusicSource.time = 0;
            comp.VinylMusicSource.Play();
        }

        private static void Jump(VinylMusicComponent comp, VinylJumpMsg msg)
        {
            var currentRelativePosition = comp.VinylMusicSource.time / comp.VinylMusicSource.clip.length;
            var newRelativePosition = currentRelativePosition - msg.JumpAmountInPercent;
            var newAbsolutePosition = newRelativePosition * comp.VinylMusicSource.clip.length;

            if (newAbsolutePosition < 0)
            {
                newAbsolutePosition = 0;
            }

            comp.VinylMusicSource.time = newAbsolutePosition;
            "vinyl_record_jump".Play();
        }
    }
}
