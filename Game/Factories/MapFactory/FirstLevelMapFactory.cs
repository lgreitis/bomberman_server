using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.BombModels;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Randomizers;

namespace GameServices.Factories.MapFactory
{
    public class FirstLevelMapFactory : MapAbstractFactory
    {
        public override List<MapPlayer> GetPlayers()
        {
            return Enumerable.Range(0, 4)
                .Select(x => new MapPlayer
                {
                    Position = new PositionExtended(0, 0),
                    Bomb = new RegularBomb()
                })
                .ToList();
        }

        public override List<MapProp> GetProps()
        {
            return new List<MapProp>();
        }

        public override List<IMapTile> GetTiles()
        {
            return MapTileRandomizer.GetMapTiles(32, 24, new List<MapTileType>
            {
                MapTileType.Cobblestone,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Ice,
                MapTileType.Water,
                MapTileType.Sand
            });
        }
    }
}
