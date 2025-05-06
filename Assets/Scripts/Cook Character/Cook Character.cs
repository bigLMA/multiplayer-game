using UnityEngine;
using PlayerController.ControllableObject;

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
            throw new System.NotImplementedException();
        }

        public void OnInteract()
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(Vector2 dir)=>direction = dir;
    }
}
