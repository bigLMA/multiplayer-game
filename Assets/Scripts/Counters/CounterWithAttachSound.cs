using Attach;
using Misc.Sound;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Counters
{
    /// <summary>
    /// This counter will produce sound on attach
    /// </summary>
    public class CounterWithAttachSound : CounterVisualInteractable
    {
        [SerializeField]
        private List<AudioResource> attachSounds;

        private IPlaySound playAttachSound;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected virtual void Start()
        {
            playAttachSound = new PlayRandomSound(GetComponent<AudioSource>(), attachSounds);

            var attachComp = GetComponent<IAttach>();
            attachComp.OnAttach += playAttachSound.Play;
        }
    }
}