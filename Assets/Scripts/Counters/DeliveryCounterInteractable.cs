using UnityEngine;
using Interaction;
using Attach;
using DishContainer;
using System.Collections;
using Dish;
using Dish.Recipe;
using Misc.Sound;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Counters
{
    public class DeliveryCounterInteractable : CounterWithAttachSound
    {
        [SerializeField]
        [Range(0.5f, 3f)]
        private float dishDestroyDelay = 1f;

        [SerializeField]
        private List<AudioResource> deliverySounds;

        [SerializeField]
        private FadingItem successImage;
        [SerializeField]
        private FadingItem failureImage;

        private bool evaluating = false;

        private IPlaySound<string> playDeliverySound;

        private AudioSource audioSource;    

        protected override void Start()
        {
            base.Start();

            audioSource = GetComponent<AudioSource>();
            playDeliverySound = new PlayConcreteSoundOutOfSubset(audioSource, deliverySounds);

            RecipeManager.instance.OnCompareSuccess += DisplaySuccess;
            RecipeManager.instance.OnCompareFail += DisplayFail;
        }

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

        private void DisplaySuccess()
        {
            playDeliverySound.Play("SFX_delivery_success");
            successImage.Fade();
        }

        private void DisplayFail()
        {
            playDeliverySound.Play("SFX_delivery_fail");
            failureImage.Fade();
        }
    }
}
