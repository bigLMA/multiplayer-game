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

        public override void AltInteract()
        {
            if (fryProduct == null) return;

            frying= !frying;

            if (frying)
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
            fryProduct.StartFrying();
            stoveOnVisual.SetActive(true);
            sizzlingParticles.SetActive(true);
            var particles = sizzlingParticles.GetComponent<ParticleSystem>();
            particles.Play();
        }

        private void StopFrying()
        {
            fryProduct.StopFrying();
            stoveOnVisual.SetActive(false);
            sizzlingParticles.SetActive(false);
            var particles = sizzlingParticles.GetComponent<ParticleSystem>();
            particles.Stop();
        }
    }

}