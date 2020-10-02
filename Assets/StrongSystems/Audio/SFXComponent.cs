using SystemBase;
using UnityEngine;

namespace StrongSystems.Audio
{
    public class SFXComponent : GameComponent
    {
        [Range(0f, 2f)]
        public float MaxPitchChange = 0.25f;

        public SoundFile[] Sounds;
    }
}