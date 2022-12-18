using GameServices.Enums;
using GameServices.Mediator;
using GameServices.Models.CommonModels;
using GameServices.Models.Containers;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
using GameServices.Models.PlayerModels;
using GameServices.TemplateMethod;

namespace GameServices.Facade.Subsystems
{
    public class MapPlayerSubsystem
    {
        private MapPlayerContainer _container = new MapPlayerContainer();

        public BombExplosionTemplate GetBombExplosionTemplate()
        {
            return _container;
        }

        public void Set(List<MapPlayer> mapPlayers)
        {
            _container.Players = mapPlayers;
        }

        public void RegisterClient(Client client, Chatroom chatroom)
        {
            var registeredClient = _container.Players.FirstOrDefault(x =>
                    x.Client != null
                    && x.Client.Token == client.Token);

            if (registeredClient == null)
            {
                _container.Players.First(x => x.Client == null).Client = client;
                client.ChatParticipant = new GameRoomClient(chatroom, $"{client.Username} (ID:{client.UserId})");
                client.ChatParticipant.Send("joined the game");

                return;
            }

            registeredClient.Client.SessionId = client.SessionId;
            registeredClient.Client.ChatParticipant.Send("reconnected");
        }

        public List<string> GetClientSessionIds()
        {
            return _container.Players
                .Where(x => x.Client != null)
                .Select(x => x.Client.SessionId)
                .ToList();
        }

        public List<object> GetPlayerData()
        {
            return _container.Players
                .Where(x => x.Client != null)
                .Select(x => new
                {
                    Username = x.Client.Username,
                    UserId = x.Client.UserId,
                    Token = x.Client.Token,
                    X = x.Position.X,
                    Y = x.Position.Y,
                    HealthPoints = x.GetHealth()
                })
                .Select(x => (object)x)
                .ToList();
        }

        public MapPlayer GetPlayerData(string sessionId)
        {
            return _container.Players.First(x => x.Client != null && x.Client.SessionId == sessionId);
        }

        public bool IsValidPlayerSessionId(string sessionId)
        {
            return _container.Players.Any(x => x.Client != null && x.Client.SessionId == sessionId);
        }

        public List<MapTexture> GetTextures()
        {
            return _container.Players
                .Where(x => x.GetBomb().IsPlaced)
                .Select(x => new MapTexture
                {
                    Position = x.GetBomb().PlacedPosition,
                    TextureType = TextureType.RegularBomb
                })
                .ToList();
        }

        public void HarmPlayers(List<Position> affectedPositions)
        {
            var affectedPlayers = _container.Players
                .Where(x => affectedPositions.Any(y =>
                    y.X == (int)x.Position.X
                    && y.Y == (int)x.Position.Y))
                .ToList();

            foreach (var affectedPlayer in affectedPlayers)
            {
                MapPlayer newPlayer;

                if (affectedPlayer is DeadPlayer)
                {
                    continue;
                }
                else if (affectedPlayer is BleedingPlayer)
                {
                    newPlayer = new InjuredPlayer(affectedPlayer);
                }
                else if (affectedPlayer is InjuredPlayer)
                {
                    newPlayer = new DeadPlayer(affectedPlayer);
                }
                else
                {
                    newPlayer = new BleedingPlayer(affectedPlayer);
                }

                _container.Players.Remove(affectedPlayer);
                _container.Players.Add(newPlayer);
            }
        }

        public Participant? GetChatParticipant(string sessionId)
        {
            var participant = _container.Players.FirstOrDefault(x => x.Client != null && x.Client.SessionId == sessionId);

            if (participant == null)
            {
                return null;
            }

            return participant.Client.ChatParticipant;
        }

        public Participant? GetChatParticipantByUsername(string username)
        {
            var participant = _container.Players.FirstOrDefault(x => x.Client != null && x.Client.Username == username);

            if (participant == null)
            {
                return null;
            }

            return participant.Client.ChatParticipant;
        }

        public MapPlayerContainer GetContainer()
        {
            return _container;
        }

        public void SetContainer(MapPlayerContainer container)
        {
            _container = container;
        }

        public List<MapPlayer> GetPlayers()
        {
            return _container.Players;
        }
    }
}
