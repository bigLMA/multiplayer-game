using UnityEngine;
using Interaction;
using Attach;
using Dish.FryProducts;

namespace Counters
{
    public class StoveCounterInteractable : CounterVisualInteractable
    {
        [SerializeField]
        private GameObject stoveOnVisual;

        [SerializeField]
        private GameObject sizzlingParticles;

        private bool frying = false;

        private FryProductBase fryProduct = null;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
        }

        private void StopFrying()
        {
            stoveOnVisual.SetActive(false);

            sizzlingParticles.SetActive(false);
            var particles = sizzlingParticles.GetComponent<ParticleSystem>();
            particles.Stop();
            audioSource.Stop();
        }
    }

}