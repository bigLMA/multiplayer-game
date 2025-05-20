using UnityEngine;
using PlayerController.ControllableObject;
using Interaction;
using Components.Moving;

namespace CookCharacter
{
    public class CookCharacter : MonoBehaviour, IControllableObject
    {
        private Interactor interactor;

        private MovingComponent movingComponent;

        void Awake()
        {
            interactor = GetComponent<Interactor>();
            movingComponent = GetComponent<MovingComponent>();
        }

        public void OnAltInteract()
        {
            interactor.AltInteract();
        }

        public void OnInteract()
        {
            interactor.Interact();
        }

        public void OnMove(Vector2 dir)=> movingComponent.direction = dir;
    }
}
