using GameServices.Builders;
using GameServices.Command;
using GameServices.Enums;
using GameServices.Facade;
using GameServices.Factories.MapFactory;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.PlayerModels;

namespace GameServices.Models.ManagerModels
{
    public class GameManager
    {
        public readonly object Lock = new object();
        public int LobbyId { get; private set; }
        public MapFacade? Map { get; private set; }
        public Level Level { get; private set; }

        public GameManager(int lobbyId)
        {
            LobbyId = lobbyId;
            Level = Level.First;

            InitializeLevel();
        }

        private void InitializeLevel()
        {
            var director = new MapDirector
            {
                Builder = new MapBuilder(MapFactory.GetMapFactory(Level))
            };

            Map = director.BuildMap();

            switch (Level)
            {
                case Level.First:
                    Level = Level.Second;
                    break;
                case Level.Second:
                    Level = Level.Third;
                    break;
                case Level.Third:
                    Level = Level.First;
                    break;
            }
        }

        public void RegisterClient(Client client)
        {
            lock (Lock)
            {
                Map.RegisterClient(client);
            }
        }

        public List<string> GetSessionIds()
        {
            lock (Lock)
            {
                return Map.GetClientSessionIds();
            }
        }

        public List<object> GetPlayers()
        {
            lock (Lock)
            {
                return Map.GetPlayerData();
            }
        }

        public MapPlayer GetPlayer(string sessionId)
        {
            lock (Lock)
            {
                return Map.GetPlayerData(sessionId);
            }
        }

        public List<IMapTile> GetMapTiles()
        {
            lock (Lock)
            {
                return Map.GetMapTiles();
            }
        }

        public void InvokeCommand(ICommand command)
        {
            command.Execute();
        }

        public bool IsValidSessionId(string sessionId)
        {
            lock (Lock)
            {
                return Map.IsValidPlayerSessionId(sessionId);
            }
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            lock (Lock)
            {
                return Map.GetMapTile(posX, posY);
            }
        }

        public List<MapTexture> GetTextures()
        {
            lock (Lock)
            {
                return Map.GetTextures();
            }
        }

        public void HarmPlayers(List<Position> affectedPositions)
        {
            lock (Lock)
            {
                Map.HarmPlayers(affectedPositions);
            }
        }

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            lock (Lock)
            {
                Map.HarmMapTiles(affectedPositions);
            }
        }
    }
}
