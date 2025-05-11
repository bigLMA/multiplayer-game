using Interaction;
using UnityEngine;

namespace Counters
{
    public class KitchenCounterInteractable : CounterVisualInteractable
    {
        public override void AltInteract()
        {
            print("alt interact");
        }

        public override void Interact(Interactor interactor)
        {
            print("interact");
        }
    }
}
