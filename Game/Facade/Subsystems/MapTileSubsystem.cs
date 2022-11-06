using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;

namespace GameServices.Facade.Subsystems
{
    public class MapTileSubsystem
    {
        private List<IMapTile> MapTiles { get; set; } = new List<IMapTile>();

        public void Set(List<IMapTile> mapTiles)
        {
            MapTiles = mapTiles;
        }

        public bool DoMapTilesExists()
        {
            return MapTiles != null && MapTiles.Any();
        }

        public List<IMapTile> GetMapTiles()
        {
            return MapTiles;
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            return MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY);
        }

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            var affectedMapTiles = MapTiles
                .Where(x => affectedPositions.Any(y =>
                    y.X == x.Position.X
                    && y.Y == x.Position.Y))
                .ToList();

            affectedMapTiles.ForEach(x => x.Explode());
        }
    }
}
