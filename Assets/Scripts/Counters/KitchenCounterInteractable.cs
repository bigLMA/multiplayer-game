using Attach;
using Interaction;
using UnityEngine;

namespace Counters
{
    public class KitchenCounterInteractable : CounterVisualInteractable
    {
        public override void Interact(Interactor interactor)
        {
            var attachComp = GetComponent<IAttach>();
            var interactorAttachComp = interactor.GetComponent<IAttach>();

            if (attachComp == null || interactorAttachComp == null) return;

            interactorAttachComp.Swap(attachComp);
        }
    }
}
