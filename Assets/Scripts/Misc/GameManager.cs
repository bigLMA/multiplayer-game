using System.Collections;
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
        }

        public void LoadScene(string levelName)
        {
            StartCoroutine(LoadSceneAsync(levelName));
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