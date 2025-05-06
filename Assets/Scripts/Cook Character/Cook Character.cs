using UnityEngine;
using PlayerController.ControllableObject;
using Components.Moving;

namespace CookCharacter
{
    public class CookCharacter : MonoBehaviour, IControllableObject
    {
        private MovingComponent movingComponent;

        void Awake()
        {
            movingComponent = GetComponent<MovingComponent>();
        }

        public void OnAltInteract()
        {
            throw new System.NotImplementedException();
        }

        public void OnInteract()
        {
            throw new System.NotImplementedException();
        }

        // On player input reset direction
        public void OnMove(Vector2 direction)=>movingComponent.direction = direction;
    }
}
