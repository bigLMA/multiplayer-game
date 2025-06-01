using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Network
{
    public class ClientButton : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                NetworkManager.Singleton.StartClient();
            });
        }
    }
}