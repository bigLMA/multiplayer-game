using MultiplayerServices;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class LobbyListUIItem : MonoBehaviour
    {
        [SerializeField]
        private Button joinLobbyButton;

        [SerializeField]
        private TextMeshProUGUI lobbyName;

        [SerializeField]
        private TextMeshProUGUI playersText;

        private Lobby lobby;

        private void Start()
        {
            joinLobbyButton.onClick.AddListener(() =>
            {
                if(lobby != null)
                {
                    LobbyManager.instance.JoinLobby(lobby);
                }
            });
        }

        public void UpdateItem(Lobby updatedLobby)
        {
            lobby = updatedLobby;

            lobbyName.text = lobby.Name;

            playersText.text = $"{lobby.Players.Count}/{lobby.MaxPlayers}";
        }
    }
}
