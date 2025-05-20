using UnityEngine;
using Interaction;
using Attach;
using Dish.FryProducts;
using Misc;
using DishContainer;
using Dish;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Collections;

namespace Counters
{
    public class StoveCounterInteractable : CounterWithAttachSound
    {
        [SerializeField]
        private GameObject stoveOnVisual;

        [SerializeField]
        private GameObject sizzlingParticles;

        private bool frying = false;

        private FryProductBase fryProduct = null;

        private AudioSource source;

        private AudioResource sizzlingLoop;

        protected override void Start()
        {
            // Bind on attach
            base.Start();

            // Get source
            source = GetComponent<AudioSource>();
            // Get given loop resource 
            sizzlingLoop = source.resource;

            // Bind reset resource on attach
            var attach = GetComponent<AttachComponent>();
            attach.OnAttach += ResetSource;
        }

        public override void Interact(Interactor interactor)
        {
            var attachComp = GetComponent<IAttach>();
            var interactorAttachComp = interactor.GetComponent<IAttach>();

            if (attachComp == null || interactorAttachComp == null) return;

            if (interactorAttachComp.attachObject != null)
            {
                if (interactorAttachComp.attachObject.TryGetComponent(out FryProductBase product))
                {
                    interactorAttachComp.Swap(attachComp);
                    fryProduct = product;
                }

                if(interactorAttachComp.attachObject.TryGetComponent(out DishContainerBase dishContainer))
                {
                    if(attachComp.attachObject.TryGetComponent(out DishProductBase dishProduct))
                    {
                        interactorAttachComp.Swap(attachComp);
                        attachComp.Detach();
                        fryProduct =null;
                    }
                }
            }
            else
            {
                if(fryProduct!=null)
                {
                    fryProduct.StopFrying();
                    fryProduct = null;
                }

                interactorAttachComp.Swap(attachComp);
            }
        }

        private void Update()
        {
            if(frying)
            {
                if (fryProduct == null) return;

                fryProduct.UpdateRoutine();
            }
        }

        public override void AltInteract()
        {
            frying= !frying;

            if(frying)
            {
                StartFrying();
            }
            else
            {
                StopFrying();
            }
        }

        private void StartFrying()
        {
            stoveOnVisual.SetActive(true);

            sizzlingParticles.SetActive(true);
            var particles = sizzlingParticles.GetComponent<ParticleSystem>();
            particles.Play();
            source.Play();
        }

        private void StopFrying()
        {
            stoveOnVisual.SetActive(false);

            sizzlingParticles.SetActive(false);
            var particles = sizzlingParticles.GetComponent<ParticleSystem>();
            particles.Stop();
            source.Stop();
        }

        private void ResetSource()
        {
            StartCoroutine(ResetSourceCoroutine());

        }

        private IEnumerator ResetSourceCoroutine()
        {
            // Wait until sound might play
            yield return new WaitForSeconds(0.14f);

            // Reset audio source
            source.Stop();
            source.resource = sizzlingLoop;
            StopAllCoroutines();
        }
    }

}