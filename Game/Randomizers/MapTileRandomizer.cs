using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.MapTiles;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace GameServices.Randomizers
{
    public static class MapTileRandomizer
    {
        public static List<IMapTile> GetMapTiles(int xSize, int ySize, List<MapTileType> tileTypes)
        {
            var mapTiles = new List<IMapTile>();
            var random = new Random();

            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    var position = new Position(x, y);
                    var mapTileType = tileTypes[random.Next(tileTypes.Count)];

                    if (x == 0 || y == 0 || x == (xSize - 1) || y == (ySize - 1))
                    {
                        mapTileType = MapTileType.Bedrock;
                    }

                    switch (mapTileType)
                    {
                        case MapTileType.Grass:
                            mapTiles.Add(new GrassTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Cobblestone:
                            mapTiles.Add(new CobblestoneTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Ice:
                            mapTiles.Add(new IceTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Sand:
                            mapTiles.Add(new SandTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Water:
                            mapTiles.Add(new WaterTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Wood:
                            mapTiles.Add(new WoodTile { Position = position, MapTileType = mapTileType });
                            break;
                        case MapTileType.Bedrock:
                        default:
                            mapTiles.Add(new BedrockTile { Position = position, MapTileType = MapTileType.Bedrock });
                            break;
                    }
                }
            }

            return mapTiles;
        }
    }
}
