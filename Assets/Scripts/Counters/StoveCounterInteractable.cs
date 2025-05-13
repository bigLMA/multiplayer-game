using UnityEngine;
using Interaction;
using Attach;

namespace Counters
{
    public class StoveCounterInteractable : CounterVisualInteractable
    {
        public override void Interact(Interactor interactor)
        {
            var attachComp = GetComponent<IAttach>();
            var interactorAttachComp = interactor.GetComponent<IAttach>();

            if (attachComp == null || interactorAttachComp == null) return;

            interactorAttachComp.Swap(attachComp);
        }

        public override void AltInteract()
        {
            base.AltInteract();
        }
    }

}