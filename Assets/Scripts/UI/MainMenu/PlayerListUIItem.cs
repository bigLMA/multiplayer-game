using MultiplayerServices;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class PlayerListUIItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI playerName;

        [SerializeField]
        private Button kickButton;

        private Player player;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            kickButton.onClick.AddListener(() =>
            {
                LobbyManager.instance.KickPlayer(player.Id);
            });
        }

        public void UpdateItem(Player newPlayer)
        {
            player = newPlayer;

            playerName.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;

            if (!LobbyManager.instance.IsLobbyHost() || LobbyManager.instance.joinedLobby.HostId == player.Id)
            {
                kickButton.gameObject.SetActive(false);
            }
        }
    }

}