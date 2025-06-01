using UnityEngine;

namespace PlayerController.ControllableObject
{
    /// <summary>
    /// Interface made to define a controllable object
    /// used in Player controller class to pass commands after player input
    /// </summary>
    public interface IControllableObject
    {
        /// <summary>
        /// Pass move input for the controlled object
        /// </summary>
        /// <param name="direction">Normalised direction for moving and rotation</param>
        public void OnMoveCall(Vector2 direction);

        /// <summary>
        /// Pass interact input for the controlled object
        /// </summary>
        public void OnInteractCall();

        /// <summary>
        /// Pass alternative interact input for the controlled object
        /// </summary>
        public void OnAltInteractCall();
    }
}