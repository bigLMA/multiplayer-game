using UnityEngine;
using Interaction;
using Attach;

namespace Counters
{
    public class PlatesCounterInteractable : CounterVisualInteractable
    {
        [Header("Plate Prefab")]
        [SerializeField]
        [Tooltip("Plate Prefab")]
        private GameObject platePrefab;

        public override void Interact(Interactor interactor)
        {
            var attachComponent = interactor.GetComponent<IAttach>();

            if (attachComponent != null)
            {
                if (attachComponent.attachObject == null)
                {
                    var go = Instantiate(platePrefab);
                    attachComponent.Attach(go);
                }
            }
        }
    }
}
