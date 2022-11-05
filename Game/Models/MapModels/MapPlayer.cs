using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.PlayerModels;

namespace GameServices.Models.MapModels
{
    public class MapPlayer
    {
        public Client? Client { get; set; }
        public PositionExtended Position { get; set; }
        public IBomb Bomb { get; set; }
        public IMapTile? MapTile { get; set; }

        public decimal GetMoveAmount(decimal baseAmount)
        {
            return MapTile.GetMoveAmount(baseAmount);
        }
    }
}
