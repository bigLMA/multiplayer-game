using PlayerController.ControllableObject;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    private void Awake()
    {
        // Initialize controllable object
        controllableObject = gameObject.GetComponent<IControllableObject>();
    }

    /// <summary>
    /// Player controller will message this object on input
    /// </summary>
    public IControllableObject controllableObject { get; set; }

    /// <summary>
    /// Message object ot move
    /// </summary>
    /// <param name="value">Input value</param>
    void OnMove(InputValue value) => controllableObject?.OnMoveCall(value.Get<Vector2>());

    /// <summary>
    /// Message object to interact
    /// </summary>
    void OnInteract() => controllableObject?.OnInteractCall();

    /// <summary>
    /// Message object to alternative interact
    /// </summary>
    void OnAltInteract() => controllableObject?.OnAltInteractCall();
}
