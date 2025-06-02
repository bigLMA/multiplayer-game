using UnityEngine;
using PlayerController.ControllableObject;
using Interaction;
using Components.Moving;
using Unity.Netcode;

namespace CookCharacter
{
    public class CookCharacter : NetworkBehaviour, IControllableObject
    {
        private Interactor interactor;

        private MovingComponent movingComponent;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

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

        public void OnMoveCall(Vector2 dir)
        {
            if (IsServer && IsLocalPlayer)
            {
                movingComponent.direction = dir;
            }
            else if (IsLocalPlayer)
            {
                MoveServerRpc(dir);
            }
        }

        [ServerRpc]
        private void MoveServerRpc(Vector2 dir)=>MoveClientRpc(dir);

        [ClientRpc]
        private void MoveClientRpc(Vector2 dir)=> movingComponent.direction = dir;
    }
}
