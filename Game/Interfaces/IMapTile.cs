using GameServices.Enums;
using GameServices.Models.CommonModels;

namespace GameServices.Interfaces
{
    public interface IMapTile
    {
        public Position Position { get; set; }
        public MapTileType MapTileType { get; set; }

        decimal GetMoveAmount(decimal baseAmount);
    }
}