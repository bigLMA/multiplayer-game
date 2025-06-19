using MultiplayerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class LobbyCreationUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField lobbyNameInputField;

        [SerializeField]
        private TMP_InputField maxPlayersInputField;

        [SerializeField]
        private Button createButton;

        [SerializeField]
        private Button cancelButton;

        private void Start()
        {
            LobbyManager.instance.OnJoinedLobby += (sender, e) => Hide(); 

            maxPlayersInputField.onSubmit.AddListener((s) =>
            {
                maxPlayersInputField.text = Mathf.Clamp(int.Parse(maxPlayersInputField.text), 2, 4).ToString();
            });

            createButton.onClick.AddListener(() =>
            {
                int maxPlayers = 0;
                bool result = int.TryParse(maxPlayersInputField.text, out maxPlayers);
                LobbyManager.instance.CreateLobby(lobbyNameInputField.text,
                                                    result?int.Parse(maxPlayersInputField.text) : 2, 
                                                    false);
            });

            cancelButton.onClick.AddListener(() =>
            {
                MainMenuUI.instance.Show();
                Hide();
            });

            // Bind to show on gos button clicked
            MainMenuUI.instance.onHostButtonClick += MainMenuUI_onHostButtonClick;

            // Hide object
            Hide();
        }

        private void MainMenuUI_onHostButtonClick(object sender, System.EventArgs e)
        {
            lobbyNameInputField.text = LobbyManager.instance.playerName +"'s Lobby";
            maxPlayersInputField.text = "";
            Show();
        }

        public void Show()=>gameObject.SetActive(true);

        public void Hide()=>gameObject.SetActive(false);
    }
}