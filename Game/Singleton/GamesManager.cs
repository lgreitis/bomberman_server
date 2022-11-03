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

        private List<GameManager> _gameManagers;

        public GamesManager()
        {
            _gameManagers = new List<GameManager>();
        }

        public void InitializeGameManager(int lobbyId)
        {
            lock (_lock)
            {
                if (!_gameManagers.Any(x => x.LobbyId == lobbyId))
                {
                    _gameManagers.Add(new GameManager(lobbyId));
                }
            }
        }

        public void RegisterClient(int lobbyId, Client client)
        {
            lock (_lock)
            {
                var gameManager = _gameManagers.Single(x => x.LobbyId == lobbyId);
                gameManager.RegisterClient(client);
            }
        }

        public GameManager GetGameManager(string sessionId)
        {
            lock (_lock)
            {
                return _gameManagers.First(x => x.IsValidSessionId(sessionId));
            }
        }
    }
}
