using SystemBase;
using Systems.GameState.Messages;
using UniRx;
using UnityEngine;
using Utils;

namespace Assets.Systems.VinylMusicSystem
{
    [GameSystem]
    public class VinylMusicSystem : GameSystem<VinylMusicComponent>
    {
        public override void Init()
        {
            //could be added to IoC
            IoC.RegisterSingleton(this);
        }

        public override void Register(VinylMusicComponent comp)
        {
            if (!comp) return;

            StartVinylMusic(comp);
            //MessageBroker.Default.Receive<GameMsgStart>().Subscribe(_ => StartVinylMusic());
        }

        private static void StartVinylMusic(VinylMusicComponent comp)
        {
            comp.VinylMusicSource.Play();
        }
    }
}
