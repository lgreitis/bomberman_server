using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels.MapTiles
{
    public class WoodTile : MapTile
    {
        public override void Explode()
        {
            MapTileType = MapTileType.Grass;
        }
    }
}
