using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public class IceTile : MapTile
    {
        public override void Explode()
        {
            MapTileType = MapTileType.Water;
        }
    }
}