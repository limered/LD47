using StrongSystems.Audio.Actions;
using UniRx;

namespace StrongSystems.Audio
{
    public static class AudioSystemExtensions
    {
        public static void Play(this string soundName, string tag = null)
        {
            MessageBroker.Default.Publish(new AudioActSFXPlay { Name = soundName, Tag = tag });
        }
    }
}