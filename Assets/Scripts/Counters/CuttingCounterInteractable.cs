using UnityEngine;
using Interaction;
using Attach;
using Dish.SlicedProducts;
using DishContainer;
using Dish;
using Misc;
using System.Collections.Generic;
using UnityEngine.Audio;
using Misc.Sound;

namespace Counters
{
    public class CuttingCounterInteractable : CounterWithAttachSound
    {
        [SerializeField]
        private List<AudioResource> sounds;

        private SliceProductBase product = null;

        private Animator animator;

        private IPlaySound playSound;

        protected override void Start()
        {
            base.Start();

            animator = GetComponent<Animator>();
            playSound = new PlayRandomSound(GetComponent<AudioSource>(), sounds);
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
                if (product != null)
                {
                    product.ResetSliceBar();
                }

                if (interactorAttachComp.attachObject== null)
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

                    if(interactorAttachComp.attachObject.TryGetComponent(out DishContainerBase container))
                    {
                        if(attachComp.attachObject.TryGetComponent(out DishProductBase dishProduct))
                        {
                            interactorAttachComp.Swap(attachComp);
                            attachComp.Detach();

                            return;
                        }
                    }
                }
            }
        }

        public override void AltInteract()
        {
            if(product ==null) return;

            animator.SetTrigger("Cut");
            product?.Slice();

            playSound.Play();
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