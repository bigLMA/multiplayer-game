using Misc;
using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Level
{
    public class EndLevel : NetworkBehaviour
    {
        [field: SerializeField]
        [Tooltip("Level duration in minutes")]
        [Range(0.1f, 10f)]
        public float levelDurationMins { get; private set; } = 0.1f;

        public float levelDurationSec { get; private set; }

        public static EndLevel instance { get; private set; } = null;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            levelDurationSec = levelDurationMins * 60f;

            NetworkManager.SceneManager.OnLoadComplete += (a, b, c) =>
            {
                if (IsServer) StartCoroutine(EndLevelCoroutine());
            };
        }

        [Rpc(SendTo.ClientsAndHost)]
        public void LevelEndRpc()
        {
            Time.timeScale = 0f;
        }

        private IEnumerator EndLevelCoroutine()
        {
            while (levelDurationSec > 0)
            {
                yield return new WaitForFixedUpdate();

                TickTimerRpc();
            }

            GameManager.instance.LoadScene("Results");
            //LevelEndRpc();
        }

        [Rpc(SendTo.ClientsAndHost)]
        private void TickTimerRpc()
        {
            levelDurationSec -= Time.fixedDeltaTime;
        }
    }
}