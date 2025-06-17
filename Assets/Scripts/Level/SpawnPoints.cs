using Unity.Netcode;
using UnityEngine;

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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(IsServer)
            {
                NetworkManager.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;    
            }
        }

        private void NetworkManager_OnClientConnectedCallback(ulong id)
        {
            var transform = NetworkManager.Singleton.ConnectedClients[id].PlayerObject.transform;
            Spawn(transform, (int)id);
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