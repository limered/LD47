using SystemBase;
using Assets.GameState.Messages;
using UniRx;
using UnityEngine;

namespace Assets.Systems.VinylMusicSystem
{
    [GameSystem]
    public class VinylMusicSystem : GameSystem<VinylMusicComponent>
    {
        public override void Register(VinylMusicComponent comp)
        {
            StartVinylMusic(comp);
            //MessageBroker.Default.Receive<GameMsgStart>().Subscribe(_ => StartVinylMusic(comp));

            MessageBroker.Default.Receive<VinylJumpMsg>().Subscribe(msg => Jump(comp, msg));
            MessageBroker.Default.Receive<NeedleChangeSpeedMsg>().Subscribe(msg => ChangeSpeed(comp, msg));

            SystemUpdate(comp).Subscribe(UpdateMusicProgress).AddTo(comp);
        }

        private void ChangeSpeed(VinylMusicComponent comp, NeedleChangeSpeedMsg msg)
        {
            comp.VinylMusicSource.pitch += msg.SpeedChangeAmount;
        }

        private static void UpdateMusicProgress(VinylMusicComponent comp)
        {
            comp.MusicProgress.Value = comp.VinylMusicSource.time / comp.VinylMusicSource.clip.length;
        }

        private static void StartVinylMusic(VinylMusicComponent comp)
        {
            comp.VinylMusicSource.Play();
        }

        private static void Jump(VinylMusicComponent comp, VinylJumpMsg msg)
        {
            var old = comp.VinylMusicSource.time;
            var currentRelativePosition = comp.VinylMusicSource.time / comp.VinylMusicSource.clip.length;
            var newRelativePosition = currentRelativePosition - msg.JumpAmountInPercent;
            var newAbsolutePosition = newRelativePosition * comp.VinylMusicSource.clip.length;

            if (newAbsolutePosition < 0)
            {
                newAbsolutePosition = 0;
            }

            Debug.Log("jumped: " + (old - newAbsolutePosition));
            comp.VinylMusicSource.time = newAbsolutePosition;
        }
    }
}
