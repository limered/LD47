using UnityEngine;

namespace StrongSystems.Audio.Actions
{
    public class AudioActSFXSetVolume
    {
        public AudioActSFXSetVolume(float volume)
        {
            Volume = Mathf.Clamp(volume, 0f, 1f);
        }

        public float Volume { get; private set; }
    }
}