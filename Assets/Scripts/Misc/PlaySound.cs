using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Misc
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField]
        private List<AudioResource> sounds = new();

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            if (sounds.Count == 0) return;
            if(audioSource==null) return;

            var rand = Random.Range(0, sounds.Count);

            audioSource.resource = sounds[rand];
            audioSource.Play();
        }

        public void Stop()
        {
            if(audioSource==null)return;

            audioSource.Stop();
        }
    }
}
