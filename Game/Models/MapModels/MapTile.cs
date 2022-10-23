using GameServices.Enums;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public class MapTile
    {
        public MapTileType MapTileType { get; set; }
        public Position Position { get; set; }
    }
}
