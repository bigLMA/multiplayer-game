using PlayerController.ControllableObject;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        [Header("Controlled GO")]
        [Tooltip("This GO will be controlled by the player controller on awake.\n\nGO script MUST implement IControllableObject interface!")]
        private GameObject controllableScript;

        private void Awake()
        {
            // Initialize controllable object
            controllableObject = controllableScript.GetComponent<IControllableObject>();
        }

        /// <summary>
        /// Player controller will message this object on input
        /// </summary>
        public IControllableObject controllableObject { get; set; }

        /// <summary>
        /// Message object ot move
        /// </summary>
        /// <param name="value">Input value</param>
        void OnMove(InputValue value) => controllableObject?.OnMove(value.Get<Vector2>());

        /// <summary>
        /// Message object to interact
        /// </summary>
        void OnInteract() => controllableObject?.OnInteract();

        /// <summary>
        /// Message object to alternative interact
        /// </summary>
        void OnAltInteract()=>controllableObject?.OnAltInteract();
    }
}