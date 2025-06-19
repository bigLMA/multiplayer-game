using Misc;
using MultiplayerServices;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        public static MainMenuUI instance;

        [SerializeField]
        private Button quickJoinButton;

        [SerializeField]
        private Button hostButton;

        [SerializeField]
        private Button lobbiesButton;

        [SerializeField]
        private Button quitButton;

        public event EventHandler onHostButtonClick;
        public event EventHandler onLobbiesButtonClick;

        void Awake()
        {
            instance = this;

            // Quick join button
            quickJoinButton.onClick.AddListener(() =>
            {
                // Try to quick join lobby
                LobbyManager.instance.OnJoinedLobby += LobbyManager_OnJoinedLobby;
                LobbyManager.instance.QuickJoinLobby();
            });

            // Host button
            hostButton.onClick.AddListener(() =>
            {
                // Hide Main Menu UI
                Hide();

                onHostButtonClick?.Invoke(this, EventArgs.Empty);
            });

            // Lobbies button
            lobbiesButton.onClick.AddListener(() =>
            {
                // Hide Main Menu UI
                Hide();

                onLobbiesButtonClick?.Invoke(this, EventArgs.Empty);
            });

            // Quit button
            quitButton.onClick.AddListener(() =>
            {
                // Quit game
                GameManager.instance.Quit();
            });
        }

        private void LobbyManager_OnJoinedLobby(object sender, LobbyManager.LobbyEventArgs e)
        {
            // Hide Main Menu UI
            Hide();

            // Unbind from delegate
            LobbyManager.instance.OnJoinedLobby-= LobbyManager_OnJoinedLobby;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}