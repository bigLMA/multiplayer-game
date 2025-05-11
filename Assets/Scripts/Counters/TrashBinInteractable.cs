using UnityEngine;
using Interaction;
using Attach;

namespace Counters
{
    public class TrashBinInteractable : CounterVisualInteractable
    {
        public override void Interact(Interactor interactor)
        {
            var interactorAttachComp = interactor.GetComponent<AttachComponent>();
            interactorAttachComp.DestroyAttached();
        }
    }
}