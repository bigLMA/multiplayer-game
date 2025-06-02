using PlayerController.ControllableObject;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : NetworkBehaviour
{
    /// <summary>
    /// Player controller will message this object on input
    /// </summary>
    public IControllableObject controllableObject { get; set; }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Initialize controllable object
        controllableObject = gameObject.GetComponent<IControllableObject>();
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = Vector2.zero;

        if(Input.GetKey(KeyCode.W))
        {
            moveVector.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVector.y = -1;
        }

        if(Input.GetKey(KeyCode.D))
        {
            moveVector.x = 1;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moveVector.x = -1;
        }

        if(IsServer&& IsLocalPlayer)
        {
            controllableObject?.OnMoveCall(moveVector.normalized);
        }
        else if(IsLocalPlayer)
        {
            MoveServerRpc(moveVector);
        }
    }

    [ServerRpc]
    private void MoveServerRpc(Vector2 moveVector)
    {
        MoveClientRpc(moveVector);
    }

    [ClientRpc]
    private void MoveClientRpc(Vector2 moveVector)
    {
        controllableObject?.OnMoveCall(moveVector.normalized);
    }

    ///// <summary>
    ///// Message object ot move
    ///// </summary>
    ///// <param name="value">Input value</param>
    //void OnMove(InputValue value) => controllableObject?.OnMoveCall(value.Get<Vector2>());

    ///// <summary>
    ///// Message object to interact
    ///// </summary>
    //void OnInteract() => controllableObject?.OnInteractCall();

    ///// <summary>
    ///// Message object to alternative interact
    ///// </summary>
    //void OnAltInteract() => controllableObject?.OnAltInteractCall();
}
