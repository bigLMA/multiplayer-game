namespace Interaction
{
    /// <summary>
    /// This interface serves for both visual representation of interactable objects
    /// and starting of interaction
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// When player controlled object "looks" at interactable object, slightly change it's appearance
        /// </summary>
        void OnLook();

        /// <summary>
        /// Revert appearance changes when controlled object not "looks" at interactable
        /// </summary>
        void OnNotLook();

        /// <summary>
        /// Primary interaction function
        /// </summary>
        void Interact();

        /// <summary>
        /// This function is not neccessary to implement, for interactables with multiple interaction ways
        /// </summary>
        void AltInteract();
    }
}