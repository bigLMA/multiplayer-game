using Level;
using Misc;
using MultiplayerServices;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Results
{
    public class ResultsUI : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI successText;

        [SerializeField]
        private TextMeshProUGUI failedText;

        [Header("Buttons")]
        //[SerializeField]
        //private Button restartButton;

        //[SerializeField]
        //private Button backToMenuButton;

        [SerializeField]
        private Button quitButton;

        void Start()
        {
            successText.text = "Successful Recipes: " + StatsManager.instance.recipesSuccessful;
            failedText.text = "Failed Recipes: " + StatsManager.instance.recipesFailed;

            //if(!NetworkManager.Singleton.IsServer)
            //{
            //    restartButton.gameObject.SetActive(false);
            //}

            //restartButton.onClick.AddListener(() =>
            //{
            //    if(NetworkManager.Singleton.IsServer)
            //    {
            //        GameManager.instance.LoadScene("Gameplay");
            //    }
            //});

            //backToMenuButton.onClick.AddListener(() =>
            //{
            //    if (LobbyManager.instance.IsLobbyHost())
            //    {
            //        LobbyManager.instance.DestroyLobby();
            //    }
            //    else
            //    {
            //        LobbyManager.instance.LeaveLobby();
            //    }

            //    GameManager.instance.LoadScene("Main Menu");
            //});

            quitButton.onClick.AddListener(() =>
            {
                GameManager.instance.Quit();
            });
        }
    }
}