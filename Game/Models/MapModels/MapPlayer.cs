using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.PlayerModels;

namespace GameServices.Models.MapModels
{
    public class MapPlayer : Player
    {
        public Client? Client { get; set; }
        public PositionExtended Position { get; set; }
        public IMapTile? MapTile { get; set; }
        public bool HasProp { get; set; } = false;

        public MapPlayer(
            Client? client,
            PositionExtended position,
            IBomb bomb,
            IMapTile? mapTile)
        {
            Client = client;
            Position = position;
            Bomb = bomb;
            MapTile = mapTile;
        }

        public MapPlayer(MapPlayer mapPlayer)
            : this(mapPlayer.Client, mapPlayer.Position, mapPlayer.Bomb, mapPlayer.MapTile)
        {
        }

        public decimal GetMoveAmount(decimal baseAmount)
        {
            return MapTile.GetMoveAmount(baseAmount);
        }

        public override int GetHealth()
        {
            return 3;
        }

        public override List<Position> ExplodeBomb()
        {
            return Bomb.Activate();
        }

        public IBomb GetBomb()
        {
            return Bomb;
        }

        public void SetBomb(IBomb bomb)
        {
            Bomb = bomb;
        }

        public void SaveBombState()
        {
            if (SavedBombState != null)
            {
                return;
            }

            SavedBombState = (IBomb)Bomb.DeepCopy();
        }

        public void ResetBombState()
        {
            if (SavedBombState != null)
            {
                Bomb = (IBomb)SavedBombState.DeepCopy();
            }

            RemoveBombState();
        }

        public void RemoveBombState()
        {
            SavedBombState = null;
        }
    }
}
