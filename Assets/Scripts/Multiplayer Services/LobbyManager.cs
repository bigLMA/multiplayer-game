using System.Collections.Generic;
using System;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;

namespace MultiplayerServices
{
    public class LobbyManager : MonoBehaviour
    {
        public static LobbyManager instance { get; private set; }

        public const string KEY_PLAYER_NAME = "PlayerName";
        public const string KEY_START_GAME = "StartGame";

        public event EventHandler OnLeftLobby;

        public event EventHandler OnGameStarted;

        public event EventHandler<LobbyEventArgs> OnJoinedLobby;
        public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;
        public event EventHandler<LobbyEventArgs> OnKickedFromLobby;
        public event EventHandler<LobbyEventArgs> OnLobbyGameModeChanged;
        public class LobbyEventArgs : EventArgs
        {
            public Lobby lobby;
        }

        public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
        public class OnLobbyListChangedEventArgs : EventArgs
        {
            public List<Lobby> lobbyList;
        }

        private float heartbeatTimer = 0f;
        private float lobbyPollTimer = 0f;
        private float refreshLobbyListTimer = 5f;
        public Lobby joinedLobby { get; private set; } = null;
        private string playerName;

        private void Awake()
        {
            Authenticate("Player");
            instance = this;
        }

        private void Update()
        {
            //HandleRefreshLobbyList(); // Disabled due to testing with multiple buiulkds
            HandleRefreshHeartbeat();
            HandleRefreshPolling();
        }

        public async void Authenticate(string authPlayerName)
        {
            playerName = authPlayerName;
            var initOpts = new InitializationOptions();
            initOpts.SetProfile(playerName);

            await UnityServices.InitializeAsync(initOpts);

            AuthenticationService.Instance.SignedIn += () =>
            {
                RefreshLobbyList();
            };

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        private void HandleRefreshLobbyList()
        {
            if(UnityServices.State==ServicesInitializationState.Initialized &&
                AuthenticationService.Instance.IsSignedIn)
            {
                refreshLobbyListTimer -= Time.deltaTime;

                if(refreshLobbyListTimer<=0f)
                {
                    refreshLobbyListTimer = 5f;
                    RefreshLobbyList();
                }
            }
        }

        private async void HandleRefreshHeartbeat()
        {
            if (joinedLobby == null) return;

            if(IsLobbyHost())
            {
                heartbeatTimer -= Time.deltaTime;

                if (heartbeatTimer <= 0f)
                {
                    heartbeatTimer = 15f;
                    await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
                }
            }
        }

        private async void HandleRefreshPolling()
        {
            if (joinedLobby == null) return;

            lobbyPollTimer -= Time.deltaTime;

            if (lobbyPollTimer <= 0f)
            {
                lobbyPollTimer = 1.1f;
                await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                OnJoinedLobbyUpdate?.Invoke(this, new() { lobby = joinedLobby });
            }

            // If player was kicked from the lobby
            if(!IsPlayerInLobby())
            {
                OnKickedFromLobby?.Invoke(this, new() { lobby = joinedLobby });
                joinedLobby =null;
            }

            // If is command to start
            if (joinedLobby.Data[KEY_START_GAME].Value!="0")
            {
                // Host already joined relay
                if(!IsLobbyHost())
                {
                    RelayManager.instance.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value);
                }

                joinedLobby = null;
                OnGameStarted?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsLobbyHost()=> joinedLobby.HostId==AuthenticationService.Instance.PlayerId;

        public bool IsPlayerInLobby()
        {
            if(joinedLobby!=null&& joinedLobby.Players!=null)
            {
                foreach(var player in joinedLobby.Players)
                {
                    if(player.Id==AuthenticationService.Instance.PlayerId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Player GetPlayer()
        {
            return new Player(AuthenticationService.Instance.PlayerId, null, new()
            {
                [KEY_PLAYER_NAME] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerName),
            });
        }

        public async void CreateLobby(string lobbyName, int maxPlayers, bool isPrivate)
        {
            var player = GetPlayer();

            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, new()
            {
                IsPrivate = isPrivate,
                Player = player,
                Data = new()
                {
                    [KEY_START_GAME] = new DataObject(DataObject.VisibilityOptions.Member, "0")
                }
            });

            joinedLobby = lobby;

            OnJoinedLobby?.Invoke(this, new() { lobby = joinedLobby });
        }

        public async void RefreshLobbyList()
        {
            try
            {
                var queryLobbyOpts = new QueryLobbiesOptions();
                queryLobbyOpts.Count = 25;

                // Order by nost available slots
                queryLobbyOpts.Order = new()
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.AvailableSlots)
                };

                var response = await LobbyService.Instance.QueryLobbiesAsync(queryLobbyOpts);

                OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs { lobbyList = response.Results });
            }
            catch(LobbyServiceException e)
            {
                print(e);
            }
        }

        public async void JoinLobbyByCode(string lobbyCode)
        {
            var player = GetPlayer();

            joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new()
            {
                Player = player,
            });

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
        }

        public async void JoinLobby(Lobby lobby)
        {
            var player = GetPlayer();

            joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, new() { Player = player});

            OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

        }

        public async void UpdatePlayerName(string newPlayerName)
        {
            playerName = newPlayerName;

            print(newPlayerName);

            if(joinedLobby!= null)
            {
                try
                {
                    var updatePlayerOpts = new UpdatePlayerOptions();
                    updatePlayerOpts.Data = new()
                    {
                        [KEY_PLAYER_NAME] = new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, newPlayerName)
                    };

                    string playerId = AuthenticationService.Instance.PlayerId;

                    joinedLobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, updatePlayerOpts);
                }
                catch (LobbyServiceException e)
                {
                    print(e);
                }
            }
        }

        public async void QuickJoinLobby()
        {
            try
            {
                joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync();

                OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
            }
            catch (LobbyServiceException e)
            {
                print(e);
            }
        }

        public async void LeaveLobby()
        {
            if (joinedLobby == null) return;

            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
                joinedLobby = null;

                OnLeftLobby?.Invoke(this, EventArgs.Empty);
            }
            catch (LobbyServiceException e)
            {
                print(e);
            }
        }

        public async void KickPlayer(string  playerId)
        {
            if(IsLobbyHost())
            {
                try
                {
                    await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
                }
                catch (LobbyServiceException e)
                {
                    print(e);
                }
            }
        }

        public async void StartGame()
        {
            if(IsLobbyHost())
            {
                try
                {
                    var relayCode = await RelayManager.instance.CreateRelay();

                    var lobby = LobbyService.Instance.UpdateLobbyAsync(joinedLobby.Id, new()
                    {
                        Data = new()
                        {
                            [KEY_START_GAME] = new DataObject(DataObject.VisibilityOptions.Member, relayCode)
                        }
                    });
                }
                catch (LobbyServiceException e)
                {
                    print(e);
                }
            }
        }
    }
}