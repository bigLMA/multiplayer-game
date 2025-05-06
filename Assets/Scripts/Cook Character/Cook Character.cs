using UnityEngine;
using PlayerController.ControllableObject;
using Interaction;

namespace CookCharacter
{
    public class CookCharacter : MonoBehaviour, IControllableObject
    {
        [Header("Moving")]
        [SerializeField]
        [Tooltip("Move speed")]
        [Range(1f, 15f)]
        private float speed = 6.5f;

        [Header("Interaction")]
        [SerializeField]
        [Tooltip("How far cook can interact")]
        [Range(0.1f, 2.3f)]
        private float interactRange;

        private Vector2 direction;

        private Interactor interactor;

        void Awake()
        {
            interactor = new(gameObject, interactRange);
        }

        // Update is called once per frame
        void Update()
        {
            if(direction != Vector2.zero)
            {
                transform.forward = new Vector3(direction.x, transform.forward.y, direction.y);
                transform.Translate(0f, 0f, Time.deltaTime * speed);
            }
        }

        public void OnAltInteract()
        {
            interactor.Interact();
        }

        public void OnInteract()
        {
            interactor.AltInteract();
        }

        public void OnMove(Vector2 dir)=>direction = dir;
    }
}
