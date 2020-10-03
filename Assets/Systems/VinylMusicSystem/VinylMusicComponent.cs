using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.VinylMusicSystem
{
    public class VinylMusicComponent : GameComponent
    {
        public AudioSource VinylMusicSource;
        public FloatReactiveProperty MusicProgress;
    }
}