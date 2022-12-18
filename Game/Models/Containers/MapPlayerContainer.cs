using System;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
using GameServices.TemplateMethod;

namespace GameServices.Models.Containers
{
    public sealed class MapPlayerContainer : BombExplosionTemplate, ICloneable
    {
        public List<MapPlayer> Players { get; set; }
        private List<int> PendingHarmPlayers { get; set; }

        public MapPlayerContainer()
        {
            Players = new List<MapPlayer>();
            PendingHarmPlayers = new List<int>();
        }

        public void KillPlayer(string username)
        {
            var player = Players.FirstOrDefault(x => x.Client != null && x.Client.Username == username);

            if (player == null)
            {
                return;
            }

            MapPlayer newPlayer = new DeadPlayer(player);
            Players.Remove(player);
            Players.Add(newPlayer);
        }

        public override void PrepareBombExplosion(List<Position> positions)
        {
            var affectedPlayers = Players
                .Where(x => positions.Any(y =>
                    y.X == (int)x.Position.X
                    && y.Y == (int)x.Position.Y)
                && x.Client != null)
                .Select(x => x.Client.UserId)
                .ToList();

            affectedPlayers.ForEach(x => PendingHarmPlayers.Add(x));
        }

        public override void ExecuteBombExplosion()
        {
            var unharmedPlayers = Players.Where(x => x.Client != null && PendingHarmPlayers.Contains(x.Client.UserId)).ToList();
            PendingHarmPlayers = new List<int>();

            foreach (var affectedPlayer in unharmedPlayers)
            {
                MapPlayer newPlayer;

                if (affectedPlayer is DeadPlayer)
                {
                    continue;
                }
                else if (affectedPlayer is BleedingPlayer)
                {
                    newPlayer = new InjuredPlayer(affectedPlayer);
                    newPlayer.Client.ChatParticipant.Send("got injured");
                }
                else if (affectedPlayer is InjuredPlayer)
                {
                    newPlayer = new DeadPlayer(affectedPlayer);
                    newPlayer.Client.ChatParticipant.Send("died");
                }
                else
                {
                    newPlayer = new BleedingPlayer(affectedPlayer);
                    newPlayer.Client.ChatParticipant.Send("got bleeding");
                }

                Players.Remove(affectedPlayer);
                Players.Add(newPlayer);
            }
        }

        public object Clone()
        {
            return (MapPlayerContainer)this.MemberwiseClone();
        }
    }
}

