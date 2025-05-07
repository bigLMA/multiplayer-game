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

        private Vector2 direction;

        private Interactor interactor;

        void Awake()
        {
            interactor = GetComponent<Interactor>();
        }

        // Update is called once per frame
        void Update()
        {
            if(direction != Vector2.zero)
            {
                transform.forward = new Vector3(direction.x, transform.forward.y, direction.y);

                if (!Physics.Raycast(transform.position + Vector3.up *0.5f, transform.forward + Vector3.up * 0.5f,0.5f))
                {
                    transform.Translate(0f, 0f, Time.deltaTime * speed);
                }
            }
        }

        public void OnAltInteract()
        {
            interactor.AltInteract();
        }

        public void OnInteract()
        {
            interactor.Interact();
        }

        public void OnMove(Vector2 dir)=>direction = dir;
    }
}
