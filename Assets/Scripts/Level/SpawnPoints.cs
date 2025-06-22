using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Level
{
    public class SpawnPoints : NetworkBehaviour
    {
        public static SpawnPoints instance;

        [SerializeField] private Transform[] spawnPoints;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void NetworkManager_OnClientConnectedCallback(ulong id)
        {
            var transform = NetworkManager.Singleton.ConnectedClients[id].PlayerObject.transform;
            Spawn(transform, (int)id);
        }

        public void RelocatePlayers()
        {
            if (!IsServer) return;

                if (NetworkManager.ConnectedClients.Count<=spawnPoints.Length)
            {
                for(ulong i = 0; (int)i<NetworkManager.ConnectedClients.Count; i++)
                {
                    var go = NetworkManager.ConnectedClients[i].PlayerObject.gameObject;
                    Spawn(go.transform, (int)i);

                    if(i==0)
                    {
                        go.GetComponent<PlayerInput>().enabled = false;
                        go.GetComponent<PlayerInput>().enabled = true;
                    }
                }
            }
        }

        public void Spawn(Transform transform, int id)
        {
            if(id<spawnPoints.Length)
            {
                transform.position = spawnPoints[id].position;
            }
        }
    }

}