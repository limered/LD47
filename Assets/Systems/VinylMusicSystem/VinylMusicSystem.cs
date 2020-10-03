using SystemBase;
using Systems.GameState.Messages;
using Assets.GameState.Messages;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;
using Utils.Plugins;

namespace Assets.Systems.VinylMusicSystem
{
    [GameSystem]
    public class VinylMusicSystem : GameSystem<VinylMusicComponent>
    {
        public override void Register(VinylMusicComponent comp)
        {
            StartVinylMusic(comp);
            //MessageBroker.Default.Receive<GameMsgStart>().Subscribe(_ => StartVinylMusic(comp));

            MessageBroker.Default.Receive<VinylJumpToMsg>().Subscribe(msg => JumpTo(comp, msg));
        }

        private static void StartVinylMusic(VinylMusicComponent comp)
        {
            comp.VinylMusicSource.Play();
        }

        private static void JumpTo(VinylMusicComponent comp, VinylJumpToMsg msg)
        {
            var absolutePosition = comp.VinylMusicSource.clip.length * msg.NewPercentalPosition;
            comp.VinylMusicSource.time = absolutePosition;
        }
    }
}
