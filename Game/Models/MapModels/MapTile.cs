using GameServices.Enums;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public abstract class MapTile : ICloneable
    {
        public MapTileType MapTileType { get; set; }
        public Position Position { get; set; }

        public object Clone()
        {
            return (MapTile)this.MemberwiseClone();
        }

        public abstract void Explode();
    }
}
