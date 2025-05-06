using UnityEngine;
using UnityEngineInternal;

namespace Interaction
{
    /// <summary>
    /// Class component to encapsulate all work with iteractable objects
    /// </summary>
    public class Interactor
    {
        /// <summary>
        /// current interactable object
        /// </summary>
        public IInteractable interactable { get; private set; } = null;

        /// <summary>
        /// Parent of the component for the component to raycast from it
        /// </summary>
        public GameObject parent;

        public float interactRange;

        public Interactor(GameObject parent, float interactRange)
        {
            this.parent = parent;
            this.interactRange = interactRange;
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;

            // Check in front of controllable object for interactibles
            if(Physics.Raycast(parent.transform.position, parent.transform.position + parent.transform.forward * interactRange, out hit))
            {
                var newInteractable = hit.collider.gameObject.GetComponent<IInteractable>();

                if(newInteractable != null)
                {
                    // Reset interactable and make necessary visual changes
                    interactable.OnNotLook();

                    interactable = newInteractable;
                    interactable.OnLook();

                    return;
                }
                else
                {
                    if (interactable != null)
                    {
                        interactable.OnNotLook();
                        interactable = null;
                    }
                }
            }
            else
            {
                if (interactable != null)
                {
                    interactable.OnNotLook();
                    interactable = null;
                }
            }
        }

        public void Interact() => interactable?.Interact();

        public void AltInteract()=> interactable?.AltInteract();
    }
}
