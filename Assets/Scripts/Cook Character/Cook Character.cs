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

        public void OnAltInteractCall()
        {
            interactor.AltInteract();
        }

        public void OnInteractCall()
        {
            interactor.Interact();
        }

        public void OnMoveCall(Vector2 dir)=> movingComponent.direction = dir;
    }
}
