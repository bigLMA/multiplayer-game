using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Misc.Sound
{
    public class PlayConcreteSoundOutOfSubset : IPlaySound<string>
    {
        private List<AudioResource> sounds;

        private AudioSource audioSource;

        public PlayConcreteSoundOutOfSubset(AudioSource source, List<AudioResource> audios)
        {
            audioSource = source;
            sounds = audios;
        }

        public void Play(string val)
        {
            if (sounds.Count == 0) return;
            if (audioSource == null) return;

            var subset = sounds.FindAll(s=>s.name.StartsWith(val));

            if(subset.Count>0)
            {
                var rand = Random.Range(0, subset.Count);

                audioSource.resource = subset[rand];
                audioSource.Play();
            }
        }

        public void Stop()
        {
            if (audioSource == null) return;

            audioSource.Stop();
        }
    }
}
