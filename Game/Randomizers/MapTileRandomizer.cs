using GameServices.Enums;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;

namespace GameServices.Randomizers
{
    public static class MapTileRandomizer
    {
        public static List<MapTile> GetMapTiles(int xSize, int ySize, List<MapTileType> tileTypes)
        {
            var mapTiles = new List<MapTile>();
            var random = new Random();

            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    var position = new Position { X = x, Y = y };
                    var mapTileType = tileTypes[random.Next(tileTypes.Count)];

                    if (x == 0 || y == 0 || x == (xSize - 1) || y == (ySize - 1))
                    {
                        mapTileType = MapTileType.Bedrock;
                    }

                    mapTiles.Add(new MapTile { Position = position, MapTileType = mapTileType });
                }
            }

            return mapTiles;
        }
    }
}
