using GameServices.Models.ManagerModels;
using Models.Behaviour.Game;
using Newtonsoft.Json;

namespace GameServices.Singleton
{
    public class GamesManager
    {
        private static object _lock = new object();
        private static GamesManager? _instance;
        public static GamesManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GamesManager();
                    }

                    return _instance;
                }
            }
        }

        public List<GameManager> GameManagers { get; private set; }

        public GamesManager()
        {
            GameManagers = new List<GameManager>();
        }

        public void InitializeGameManager(int lobbyId)
        {
            lock (_lock)
            {
                if (!GameManagers.Any(x => x.LobbyId == lobbyId))
                {
                    var gameManager = new GameManager { LobbyId = lobbyId };
                    gameManager.InitializeLevel();

                    GameManagers.Add(gameManager);
                }
            }
        }

        public void RegisterClient(int lobbyId, Client client)
        {
            lock (_lock)
            {
                var gameManager = GameManagers.Single(x => x.LobbyId == lobbyId);

                if (!gameManager.Clients.Any(x => x.Token == client.Token))
                {
                    gameManager.Clients.Add(client);
                }
            }
        }

        public string GetMap(int lobbyId)
        {
            lock (_lock)
            {
                var gameManager = GameManagers.Single(x => x.LobbyId == lobbyId);

                return gameManager.Map.MapTilesAsJson();
            }
        }

        public List<string> GetSessionIds(int lobbyId)
        {
            lock (_lock)
            {
                var gameManager = GameManagers.Single(x => x.LobbyId == lobbyId);

                return gameManager.GetSessionIds();
            }
        }

        public string GetPlayers(int lobbyId)
        {
            lock (_lock)
            {
                var gameManager = GameManagers.Single(x => x.LobbyId == lobbyId);

                return JsonConvert.SerializeObject(gameManager.Clients.Select(x => new
                {
                    Username = x.Username,
                    UserId = x.UserId,
                    Token = x.Token,
                    X = x.X,
                    Y = x.Y
                }).ToList());
            }
        }

        public int GetLobbyId(string token)
        {
            lock (_lock)
            {
                return GameManagers.First(x => x.Clients.Any(y => y.Token == token)).LobbyId;
            }
        }

        public void MovePlayer(string token, bool positiveX, bool negativeX, bool positiveY, bool negativeY)
        {
            lock (_lock)
            {
                var moveAmount = 4;
                var gameManager = GameManagers.First(x => x.Clients.Any(y => y.Token == token));
                var client = gameManager.Clients.Single(x => x.Token == token);

                if (positiveX != negativeX)
                {
                    if (positiveX)
                    {
                        client.X += moveAmount;
                    }
                    else if (negativeX)
                    {
                        client.X -= moveAmount;
                    }
                }

                if (positiveY != negativeY)
                {
                    if (positiveY)
                    {
                        client.Y += moveAmount;
                    }
                    else if (negativeY)
                    {
                        client.Y -= moveAmount;
                    }
                }
            }
        }
    }
}
