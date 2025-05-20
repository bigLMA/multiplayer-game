using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Misc
{
    /// <summary>
    /// Plays random / only sound out of given
    /// </summary>
    public class PlayRandomSound : IPlaySound
    {
        private List<AudioResource> sounds;

        private AudioSource audioSource;

        public PlayRandomSound(AudioSource source, List<AudioResource> audios)
        {
            audioSource = source;
            sounds = audios;
        }

        public void Play()
        {
            if (sounds.Count == 0) return;
            if (audioSource == null) return;

            var rand = Random.Range(0, sounds.Count);

            audioSource.resource = sounds[rand];
            audioSource.Play();
        }

        public void Stop()
        {
            if (audioSource == null) return;

            audioSource.Stop();
        }
    }
}
