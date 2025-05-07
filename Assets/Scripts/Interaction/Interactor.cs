using UnityEngine;
using UnityEngineInternal;

namespace Interaction
{
    /// <summary>
    /// Class component to encapsulate all work with iteractable objects
    /// </summary>
    public class Interactor : MonoBehaviour
    {
        /// <summary>
        /// current interactable object
        /// </summary>
        public IInteractable interactable { get; private set; } = null;

        [Header("Interaction")]
        [SerializeField]
        [Tooltip("How far object can interact")]
        [Range(0.1f, 2.3f)]
        public float interactRange = 1.15f;

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;

            // Check in front of controllable object for interactibles
            if(Physics.Raycast(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward * interactRange, out hit))
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
