using UnityEngine;

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
    public Vector2 direction { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If direction is set by player input
        if (direction != Vector2.zero)
        {
            // Set new forward point for object
            transform.forward = new Vector3(direction.x, transform.forward.y, direction.y);
            // Translate it forward
            transform.Translate(0f, 0f, Time.deltaTime * speed);
        }
    }
}
