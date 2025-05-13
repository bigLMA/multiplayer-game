using UnityEngine;
using Interaction;
using Attach;
using Dish.SlicedProducts;

namespace Counters
{
    public class CuttingCounterInteractable : CounterVisualInteractable
    {
        private SliceProductBase product = null;

        public override void Interact(Interactor interactor)
        {
            var attachComp = GetComponent<IAttach>();
            var interactorAttachComp = interactor.GetComponent<IAttach>();

            if (attachComp == null || interactorAttachComp == null) return;

            if(attachComp.attachObject==null)
            {
                if (interactorAttachComp.attachObject.TryGetComponent(out SliceProductBase sliced))
                {
                    interactorAttachComp.Swap(attachComp);
                    return;
                }
            }
            else
            {
                

                if(interactorAttachComp.attachObject== null)
                {
                    interactorAttachComp.Swap(attachComp);
                    return;
                }
                else
                {
                    if (interactorAttachComp.attachObject.TryGetComponent(out SliceProductBase sliced))
                    {
                        interactorAttachComp.Swap(attachComp);
                        return;
                    }
                }
            }

            //interactorAttachComp.Swap(attachComp);
        }

        public override void AltInteract()
        {
            base.AltInteract();
        }
    }
}