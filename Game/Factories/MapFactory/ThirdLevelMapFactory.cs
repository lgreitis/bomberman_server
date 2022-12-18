using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.BombModels;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.MapProps;
using GameServices.Randomizers;

namespace GameServices.Factories.MapFactory
{
    public class ThirdLevelMapFactory : MapAbstractFactory
    {
        public override List<MapPlayer> GetPlayers()
        {
            return Enumerable.Range(0, 2)
                .Select(x => new MapPlayer(
                    null,
                    new PositionExtended(0, 0),
                    new RegularBomb(),
                    null))
                .ToList();
        }

        public override List<IMapProp> GetProps()
        {
            var random = new Random();

            List<IMapProp> mapProps = new List<IMapProp>();

            for (int i = 0; i < 20; i++)
            {
                mapProps.Add(new CircularBombProp { Position = new Position(random.Next(1, 31), random.Next(1, 23)) });
            }

            return mapProps.DistinctBy(x => new { x.Position.X, x.Position.Y }).ToList();
        }

        public override List<MapTile> GetTiles()
        {
            return MapTileRandomizer.GetMapTiles(32, 24, new List<MapTileType>
            {
                MapTileType.Cobblestone,
                MapTileType.Wood,
                MapTileType.Grass
            });
        }
    }
}
