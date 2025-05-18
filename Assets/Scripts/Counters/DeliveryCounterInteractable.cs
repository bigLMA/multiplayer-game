using UnityEngine;
using Interaction;
using Attach;
using DishContainer;
using System.Collections;
using Dish;

namespace Counters
{
    public class DeliveryCounterInteractable : CounterVisualInteractable
    {
        [SerializeField]
        [Range(0.5f, 3f)]
        private float dishDestroyDelay = 1f;

        // TODO orders
        ////////////
        
        private bool evaluating = false;

        public override void Interact(Interactor interactor)
        {
            if (evaluating) return;

            var interactorAttach = interactor.GetComponent<IAttach>();
            var attach = GetComponent<IAttach>();

            if (interactorAttach == null || attach == null) return;

            if(interactorAttach.attachObject.TryGetComponent(out DishContainerBase container))
            {
                interactorAttach.Swap(attach);

                StartCoroutine(DishDestroyCoroutine());
            }
        }

        private IEnumerator DishDestroyCoroutine()
        {
            evaluating = true;
            //TODO evaluate dish to orders
            var attach = GetComponent<IAttach>();
            if (attach.attachObject.TryGetComponent(out DishContainerBase container))
            {
               // var dish = container.dish;
            }

            yield return new WaitForSeconds(dishDestroyDelay);

            var go = attach.attachObject;
            attach.Detach();
            Destroy(go);

            evaluating= false;
        }
    }
}
