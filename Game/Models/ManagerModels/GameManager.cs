using GameServices.Builders;
using GameServices.Command;
using GameServices.Enums;
using GameServices.Facade;
using GameServices.Factories.MapFactory;
using GameServices.Interfaces;
using GameServices.Mediator;
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
        public Chatroom Chatroom { get; private set; }
        public Participant Logger { get; private set; }

        public GameManager(int lobbyId)
        {
            LobbyId = lobbyId;
            Level = Level.First;
            Chatroom = new GameRoomConsole();
            Logger = new GameRoomServer(Chatroom);

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
                    Logger.Send("Level 1 has started.");
                    break;
                case Level.Second:
                    Level = Level.Third;
                    Logger.Send("Level 2 has started.");
                    break;
                case Level.Third:
                    Level = Level.First;
                    Logger.Send("Level 3 has started.");
                    break;
            }
        }

        public void RegisterClient(Client client)
        {
            lock (Lock)
            {
                Map.RegisterClient(client, this.Chatroom);
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

        public List<MapTile> GetMapTiles()
        {
            lock (Lock)
            {
                return Map.GetMapTiles();
            }
        }

        public void InvokeCommand(ICommand command)
        {
            lock (Lock)
            {
                command.Execute();
            }
        }

        public void RevokeCommand(ICommand command)
        {
            lock (Lock)
            {
                command.Undo();
            }
        }

        public bool IsValidSessionId(string sessionId)
        {
            lock (Lock)
            {
                return Map.IsValidPlayerSessionId(sessionId);
            }
        }

        public MapTile? GetMapTile(decimal posX, decimal posY)
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

        public void PickProp(MapPlayer player)
        {
            lock (Lock)
            {
                Map.PickProp(player);
            }
        }

        public void Log(string message, string sessionId)
        {
            lock (Lock)
            {
                var participant = Map.GetChatParticipant(sessionId);

                if (participant == null)
                {
                    return;
                }

                Logger.Send(message, participant);
            }
        }

        public void Log(string message, Participant recipient)
        {
            lock (Lock)
            {
                Logger.Send(message, recipient);
            }
        }

        public void Log(string message)
        {
            lock (Lock)
            {
                Logger.Send(message);
            }
        }

        public Participant? GetChatParticipant(string sessionId)
        {
            lock (Lock)
            {
                return Map.GetChatParticipant(sessionId);
            }
        }

        public Participant? GetChatParticipantByUsername(string username)
        {
            lock (Lock)
            {
                return Map.GetChatParticipantByUsername(username);
            }
        }

        public List<string> GetSessionMessages(string sessionId, bool allMessages)
        {
            lock (Lock)
            {
                var participant = GetChatParticipant(sessionId);

                if (participant == null)
                {
                    return new List<string>();
                }

                return Chatroom.Retrieve(participant, allMessages);
            }
        }
    }
}
