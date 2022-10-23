using GameServices.Enums;
using GameServices.Models.MapModels;
using GameServices.Randomizers;

namespace GameServices.Factories.MapFactory
{
    public class SecondLevelMapFactory : MapAbstractFactory
    {
        public override List<MapPlayer> GetPlayers()
        {
            return new List<MapPlayer>();
        }

        public override List<MapProp> GetProps()
        {
            return new List<MapProp>();
        }

        public override List<MapTile> GetTiles()
        {
            return MapTileRandomizer.GetMapTiles(32, 24, new List<MapTileType>
            {
                MapTileType.Cobblestone,
                MapTileType.Wood,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass
            });
        }
    }
}
