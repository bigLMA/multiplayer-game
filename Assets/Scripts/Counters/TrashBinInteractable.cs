using UnityEngine;
using Interaction;
using Attach;
using Misc;

namespace Counters
{
    public class TrashBinInteractable : CounterVisualInteractable
    {
        private PlaySound playSound;

        private void Start()
        {
            playSound = GetComponent<PlaySound>();
        }

        public override void Interact(Interactor interactor)
        {
            var interactorAttachComp = interactor.GetComponent<AttachComponent>();
            interactorAttachComp.DestroyAttached();
            playSound.Play();
        }
    }
}