using Models.Behaviour;
using Models.Game;
using Newtonsoft.Json;

namespace GameServer.Data
{
    public class GameManager
    {
        private static readonly object _instanceLock = new object();
        private static GameManagerInstance? _instance;
        public static GameManagerInstance Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManagerInstance();
                    }

                    return _instance;
                }
            }
        }

        public class GameManagerInstance
        {

            private List<Game> Games { get; set; }
            private readonly static object _lock = new object();
            private PlayerBuilder _playerBuilder;

            public GameManagerInstance()
            {
                this.Games = new List<Game>();
                this._playerBuilder = new PlayerBuilder();
            }

            public void InitializeGame(int lobbyId)
            {
                lock (_lock)
                {
                    var lobbyData = LobbyManager.Instance.GetLoby(lobbyId);

                    if (lobbyData == null)
                    {
                        throw new Exception("Lobby does not exist.");
                    }

                    if (Games.Any(x => x.LobbyId == lobbyData.LobbyId))
                    {
                        return;
                    }

                    var game = new Game
                    {
                        LobbyId = lobbyData.LobbyId,
                        ValidUserIds = lobbyData.UserIds
                    };

                    Games.Add(game);
                }
            }

            public void RegisterPlayer(int lobbyId, int userId, string token, string username)
            {
                lock (_lock)
                {
                    var game = Games.Single(x => x.LobbyId == lobbyId);

                    if (!game.ValidUserIds.Contains(userId))
                    {
                        throw new Exception("User is not registered in the game.");
                    }

                    if (game.Players.Any(x => x.UserId == userId || x.Token == token))
                    {
                        throw new Exception("User has been already registered into the game.");
                    }

                    _playerBuilder.Username(username);
                    _playerBuilder.UserId(userId);
                    _playerBuilder.Token(token);
                    _playerBuilder.IsConnected();
                    _playerBuilder.LocationX(0);
                    _playerBuilder.LocationY(0);
                    _playerBuilder.Health(3);

                    game.Players.Add(_playerBuilder.Build());
                }
            }

            public void MovePlayer(string token, bool posX, bool negX, bool posY, bool negY)
            {
                lock (_lock)
                {
                    var game = Games.Single(x => x.Players.Any(y => y.Token == token));

                    if (game == null)
                    {
                        throw new Exception("Invalid user.");
                    }

                    var player = game.Players.Single(x => x.Token == token);

                    if (player == null)
                    {
                        throw new Exception("Invalid user.");
                    }

                    if (posX != negX && (posX || negX))
                    {
                        player.LocationX += posX ? 10 : -10;
                    }

                    if (posY != negY && (posY || negY))
                    {
                        player.LocationY += posY ? 10 : -10;
                    }
                }
            }

            public List<Game> GetGamesData()
            {
                lock (_lock)
                {
                    return Games;
                }
            }
        }
    }
}
