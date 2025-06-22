using MultiplayerServices;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class LobbyScreenUI : MonoBehaviour
    {
        [Header("Top Section")]
        [SerializeField]
        private Button leaveButton;

        [SerializeField]
        private TextMeshProUGUI lobbyName;

        [SerializeField]
        private Button startButton;

        [Header("List")]
        [SerializeField]
        private Transform list;

        [Header("List item")]
        [SerializeField]
        private GameObject playerListUIItemPrefab;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            leaveButton.onClick.AddListener(() =>
            {
                LobbyManager.instance.LeaveLobby();
            });

            startButton.onClick.AddListener(() =>
            {
                LobbyManager.instance.StartGame();
            });

            LobbyManager.instance.OnJoinedLobby += GameManager_OnJoinedLobby;
            LobbyManager.instance.OnJoinedLobbyUpdate += GameManager_OnJoinedLobbyUpdate;
            LobbyManager.instance.OnKickedFromLobby += (sender, args) =>
            {
                ClearLobbyList();
                Hide();
            };
            LobbyManager.instance.OnLeftLobby += (sender, args) =>
            {
                ClearLobbyList();
                Hide();
            };
             

            Hide();
        }

        private void GameManager_OnJoinedLobbyUpdate(object sender, LobbyManager.LobbyEventArgs e)
        {
            startButton.gameObject.SetActive(LobbyManager.instance.IsLobbyHost());
            UpdateList(e.lobby.Players);
        }

        private void GameManager_OnJoinedLobby(object sender, LobbyManager.LobbyEventArgs e)
        {
            startButton.gameObject.SetActive(LobbyManager.instance.IsLobbyHost());
            Show();
            lobbyName.text = e.lobby.Name;
            UpdateList(e.lobby.Players);
        }

        private void UpdateList(List<Player> players)
        {
            ClearLobbyList();

            foreach (var player in players)
            {
                var item = Instantiate(playerListUIItemPrefab, list);
                item.GetComponent<PlayerListUIItem>().UpdateItem(player);
            }
        }

        private void ClearLobbyList()
        {
            foreach (Transform item in list)
            {
                Destroy(item.gameObject);
            }
        }

        public void Show()=>gameObject.SetActive(true);

        public void Hide()=>gameObject.SetActive(false);  
    }
}
