using Game.Model.Common;
using GameServices.Enums;

namespace GameServices.Model.Map
{
    public class MapTile
    {
        public MapTileType MapTileType { get; set; }
        public Position Position { get; set; }
    }
}
