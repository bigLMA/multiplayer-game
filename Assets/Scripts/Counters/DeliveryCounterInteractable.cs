using UnityEngine;
using Interaction;
using Attach;
using DishContainer;
using System.Collections;
using Dish;
using Dish.Recipe;

namespace Counters
{
    public class DeliveryCounterInteractable : CounterWithAttachSound
    {
        [SerializeField]
        [Range(0.5f, 3f)]
        private float dishDestroyDelay = 1f;
        
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

            yield return new WaitForSeconds(dishDestroyDelay);

            var attach = GetComponent<IAttach>();
            
            if(attach.attachObject.TryGetComponent(out DishContainerBase container))
            {
                RecipeManager.instance.CompareDish(container.dish);
            }

            var go = attach.attachObject;
            attach.Detach();
            Destroy(go);

            evaluating= false;
        }
    }
}
