using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public class IceTile : IMapTile
    {
        public Position Position { get; set; }
        public MapTileType MapTileType { get; set; }

        private decimal Multiplier = 1.5M;

        public decimal GetMoveAmount(decimal baseAmount)
        {
            return Multiplier * baseAmount;
        }
    }
}