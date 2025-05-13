using UnityEngine;
using Interaction;
using Attach;
using Dish.SlicedProducts;

namespace Counters
{
    public class CuttingCounterInteractable : CounterVisualInteractable
    {
        private SliceProductBase product = null;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void Interact(Interactor interactor)
        {
            var attachComp = GetComponent<IAttach>();
            var interactorAttachComp = interactor.GetComponent<IAttach>();

            if (attachComp == null || interactorAttachComp == null) return;

            if (attachComp.attachObject == null && interactorAttachComp.attachObject == null) return;

            if(attachComp.attachObject==null)
            {
                if (interactorAttachComp.attachObject.TryGetComponent(out SliceProductBase sliced))
                {
                    interactorAttachComp.Swap(attachComp);
                    
                    AttachProduct(sliced);

                    return;
                }
            }
            else
            {
                if(interactorAttachComp.attachObject== null)
                {
                    interactorAttachComp.Swap(attachComp);

                    DetachProduct();

                    return;
                }
                else
                {
                    if (interactorAttachComp.attachObject.TryGetComponent(out SliceProductBase sliced))
                    {
                        interactorAttachComp.Swap(attachComp);

                        AttachProduct(sliced);

                        return;
                    }
                }
            }
        }

        public override void AltInteract()
        {
            if(product ==null) return;

            animator.SetTrigger("Cut");
            product?.Slice();
        }

        private void OnProductSliced(GameObject prefab)
        {
            product.OnSliced -= OnProductSliced;

            var attachComp = GetComponent<IAttach>();
            attachComp.Detach();

            var slicedGO = Instantiate(product.GetSlicedPrefab());
            attachComp.Attach(slicedGO);

            Destroy(product.gameObject);
            product = null;
        }

        private void AttachProduct(SliceProductBase slicedProduct)
        {
            //if (product == null) return;

            product = slicedProduct;
            product.OnSliced += OnProductSliced;
        }

        private void DetachProduct()
        {
           /// if (product == null) return;

            product.OnSliced -= OnProductSliced;
            product= null;
        }
    }
}