using UnityEngine;
using Interaction;
using Attach;
using Misc;
using System.Collections.Generic;
using UnityEngine.Audio;
using Misc.Sound;

namespace Counters
{
    public class TrashBinInteractable : CounterVisualInteractable
    {
        [SerializeField]
        private List<AudioResource> sounds;

        private IPlaySound playSound;

        private void Start()
        {
            playSound = new PlayRandomSound(GetComponent<AudioSource>(), sounds);
        }

        public override void Interact(Interactor interactor)
        {
            var interactorAttachComp = interactor.GetComponent<AttachComponent>();
            interactorAttachComp.DestroyAttached();
            playSound.Play();
        }
    }
}