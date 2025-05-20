using Interaction;
using UnityEngine;

namespace Counters
{
    public class CounterVisualInteractable : MonoBehaviour, IInteractable
    {

        [Header("Visual switching")]
        [SerializeField]
        [Tooltip("Unselected GO")]
        private GameObject unselected;

        [SerializeField]
        [Tooltip("Selected GO")]
        private GameObject selected;

        public virtual void AltInteract() { }

        public virtual void Interact(Interactor interactor) { }

        public virtual void OnLook()
        {
            unselected.SetActive(false);
            selected.SetActive(true);
        }

        public virtual void OnNotLook()
        {
            unselected.SetActive(true);
            selected.SetActive(false);
        }
    }
}
