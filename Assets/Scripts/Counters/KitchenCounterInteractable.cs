using Attach;
using Interaction;
using UnityEngine;

namespace Counters
{
    public class KitchenCounterInteractable : CounterWithAttachSound
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
