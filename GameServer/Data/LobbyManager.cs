using Models.Behaviour.Lobby;

namespace GameServer.Data
{
    public class LobbyManager
    {
        private static readonly object _instanceLock = new object();
        private static LobbyManagerInstance? _instance;
        public static LobbyManagerInstance Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LobbyManagerInstance();
                    }

                    return _instance;
                }
            }
        }

        public class LobbyManagerInstance
        {

            private List<Lobby> Lobbies { get; set; }
            private readonly static object _lock = new object();

            public LobbyManagerInstance()
            {
                this.Lobbies = new List<Lobby>();
            }

            public void AddPlayerToLobby(int lobbyId, int userId)
            {
                lock (_lock)
                {
                    if (!Lobbies.Any(x => x.LobbyId == lobbyId))
                    {
                        Lobbies.Add(new Lobby
                        {
                            LobbyId = lobbyId
                        });
                    }

                    var lobby = Lobbies.Single(x => x.LobbyId == lobbyId);

                    if (!lobby.UserIds.Any(x => x == userId))
                    {
                        lobby.UserIds.Add(userId);
                    }
                }
            }

            public bool CanStartGame(int lobbyId)
            {
                lock (_lock)
                {
                    var lobby = Lobbies.SingleOrDefault(x => x.LobbyId == lobbyId);

                    return lobby != null && lobby.UserIds.Count >= 1;
                }
            }

            public Lobby GetLoby(int lobbyId)
            {
                lock (_lock)
                {
                    return Lobbies.Single(x => x.LobbyId == lobbyId);
                }
            }
        }
    }
}
