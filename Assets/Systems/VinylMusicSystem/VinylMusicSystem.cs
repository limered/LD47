using SystemBase;
using Assets.GameState.Messages;
using UniRx;

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

            SystemUpdate(comp).Subscribe(UpdateMusicProgress).AddTo(comp);
        }

        private static void UpdateMusicProgress(VinylMusicComponent comp)
        {
            comp.MusicProgress.Value = comp.VinylMusicSource.time / comp.VinylMusicSource.clip.length;
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
