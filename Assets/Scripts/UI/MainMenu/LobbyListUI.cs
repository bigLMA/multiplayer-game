using MultiplayerServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using System;

namespace UI.MainMenu
{
    public class LobbyListUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField]
        private Button leaveButton;

        [SerializeField]
        private Button refreshButton;

        [Header("List")]
        [SerializeField]
        private Transform list;

        [Header("List Item")]
        [SerializeField]
        private GameObject lobbyListUIItemPrefab;

        private event EventHandler onListShow;

        void Start()
        {
            MainMenuUI.instance.onLobbiesButtonClick += (sender, e) => Show();

            leaveButton.onClick.AddListener(() =>
            {
                MainMenuUI.instance.Show();
                Hide();
            });

            refreshButton.onClick.AddListener(() =>
            {
                LobbyManager.instance.RefreshLobbyList();
            });

            LobbyManager.instance.OnJoinedLobby += (sender, e)=> Hide();
            LobbyManager.instance.OnKickedFromLobby += (sender, e)=> Show();
            LobbyManager.instance.OnLeftLobby += (sender, e)=> Show();
            LobbyManager.instance.OnLobbyListChanged += LobbyManager_OnLobbyListChanged;

            onListShow += (sender, e) => LobbyManager.instance.RefreshLobbyList();

            Hide();
        }

        private void LobbyManager_OnLobbyListChanged(object sender, LobbyManager.OnLobbyListChangedEventArgs e)
        {
            UpdateList(e.lobbyList);
        }

        private void UpdateList(List<Lobby> lobbies)
        {
            foreach (Transform item in list)
            {
                Destroy(item.gameObject);
            }

            foreach(var lobby in lobbies)
            {
                var item = Instantiate(lobbyListUIItemPrefab, list);
                var itemScript = item.GetComponent<LobbyListUIItem>();

                if (itemScript != null) itemScript.UpdateItem(lobby);
            }
        }

        public void Show() { gameObject.SetActive(true); onListShow?.Invoke(this, EventArgs.Empty); }

        public void Hide()=>gameObject.SetActive(false);
    }
}