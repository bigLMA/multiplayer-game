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

        if (!IsOwner) gameObject.GetComponent<PlayerInput>().enabled = false;

        // Initialize controllable object
        controllableObject = gameObject.GetComponent<IControllableObject>();
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

    [ServerRpc]
    private void InteractServerRpc()
    {
        InteractClientRpc();
    }

    [ClientRpc]
    private void InteractClientRpc() => controllableObject?.OnInteractCall();

    [ServerRpc]
    private void AltInteractServerRpc()
    {
        AltInteractClientRpc();
    }

    [ClientRpc]
    private void AltInteractClientRpc()
    {
        controllableObject?.OnAltInteractCall();
    }

    /// <summary>
    /// Message object ot move
    /// </summary>
    /// <param name="value">Input value</param>
    void OnMove(InputValue value)
    {
        if(IsServer&& IsLocalPlayer)
        {
            controllableObject?.OnMoveCall(value.Get<Vector2>());
        }
        else if(IsLocalPlayer) 
        {
            MoveServerRpc(value.Get<Vector2>());
        }
    }

    /// <summary>
    /// Message object to interact
    /// </summary>
    void OnInteract()
    {
        if(IsServer&&IsLocalPlayer)
        {
            controllableObject?.OnInteractCall();
        }
        else if(IsLocalPlayer)
        {
            InteractServerRpc();
        }
    }

    /// <summary>
    /// Message object to alternative interact
    /// </summary>
    void OnAltInteract()
    {
        if (IsServer && IsLocalPlayer)
        {
            controllableObject?.OnAltInteractCall();
        }
        else if (IsLocalPlayer)
        {
           AltInteractServerRpc();
        }
    }
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

//private void FixedUpdate()
//{
//    Vector2 moveVector = Vector2.zero;

//    if(Input.GetKey(KeyCode.W))
//    {
//        moveVector.y = 1;
//    }
//    else if (Input.GetKey(KeyCode.S))
//    {
//        moveVector.y = -1;
//    }

//    if(Input.GetKey(KeyCode.D))
//    {
//        moveVector.x = 1;
//    }
//    else if(Input.GetKey(KeyCode.A))
//    {
//        moveVector.x = -1;
//    }

//    if(IsServer&& IsLocalPlayer)
//    {
//        controllableObject?.OnMoveCall(moveVector.normalized);
//    }
//    else if(IsLocalPlayer)
//    {
//        MoveServerRpc(moveVector);
//    }

//    if(Input.GetMouseButtonDown(0))
//    {
//        if (IsServer && IsLocalPlayer)
//        {
//            controllableObject?.OnInteractCall();
//        }
//        else if (IsLocalPlayer)
//        {
//            InteractServerRpc();
//        }

//        return;
//    }
//    else if(Input.GetMouseButtonDown(1))
//    {
//        if (IsServer && IsLocalPlayer)
//        {
//            controllableObject?.OnAltInteractCall();
//        }
//        else if (IsLocalPlayer)
//        {
//            AltInteractServerRpc();
//        }

//        return;
//    }

//    if (Input.GetMouseButtonUp(0))
//    {
//        if (IsServer && IsLocalPlayer)
//        {
//            controllableObject?.OnInteractCall();
//        }
//        else if (IsLocalPlayer)
//        {
//            InteractServerRpc();
//        }

//        return;
//    }
//    else if (Input.GetMouseButtonUp(1))
//    {
//        if (IsServer && IsLocalPlayer)
//        {
//            controllableObject?.OnAltInteractCall();
//        }
//        else if (IsLocalPlayer)
//        {
//            AltInteractServerRpc();
//        }

//        return;
//    }
//}