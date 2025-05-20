using UnityEngine;

namespace Components.Moving
{
    public class MovingComponent : MonoBehaviour
    {
        [Header("Moving")]
        [SerializeField]
        [Tooltip("Move speed")]
        [Range(1f, 15f)]
        private float speed = 6.5f;

        /// <summary>
        /// Current player direction (zero vector if player not moving)
        /// </summary>
        public Vector2 direction { get; set; } = Vector2.zero;

        /// <summary>
        /// Animator component
        /// </summary>
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // If direction is set by player input
            if (direction != Vector2.zero)
            {
                // Turn object toward new forward
                transform.forward = new Vector3(direction.x, transform.forward.y, direction.y);

                // If object is not facing obstacle
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward + Vector3.up * 0.5f, 0.5f))
                {
                    // Translate object forward
                    transform.Translate(0f, 0f, Time.deltaTime * speed);

                    // Play walking animation
                    animator.SetBool("IsWalking", true);
                }
            }
            else // Otherwise
            {
                // Play idle animation
                animator.SetBool("IsWalking", false);
            }
        }
    }
}
