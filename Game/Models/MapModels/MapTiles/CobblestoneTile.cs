using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public class CobblestoneTile : IMapTile
    {
        public Position Position { get; set; }
        public MapTileType MapTileType { get; set; }

        private decimal Multiplier = 0.0M;

        public decimal GetMoveAmount(decimal baseAmount)
        {
            return Multiplier * baseAmount;
        }

        public void Explode()
        {
        }
    }
}