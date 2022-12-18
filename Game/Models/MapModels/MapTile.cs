using GameServices.Enums;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public abstract class MapTile
    {
        public MapTileType MapTileType { get; set; }
        public Position Position { get; set; }

        public abstract void Explode();
    }
}
