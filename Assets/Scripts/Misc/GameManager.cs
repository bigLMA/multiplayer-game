using Level;
using MultiplayerServices;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(instance.gameObject);
                }
            }

            LobbyManager.instance.OnGameStarted += LobyManager_OnGameStarted;
        }

        private void LobyManager_OnGameStarted(object sender, System.EventArgs e)
        {
            LoadScene("Gameplay");
        }

        public void LoadScene(string levelName)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneManager_OnLoadComplete;
           // StartCoroutine(LoadSceneAsync(levelName));
        }

        private void SceneManager_OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
        {
            SpawnPoints.instance.RelocatePlayers();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }

        private IEnumerator LoadSceneAsync(string levelName)
        {
            var loadOp = SceneManager.LoadSceneAsync(levelName);
            loadOp.allowSceneActivation = false;

            while (!loadOp.isDone)
            {
                if (loadOp.progress >= 0.9f)
                {
                    loadOp.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }

}