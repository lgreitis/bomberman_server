using Game.Model.Common;
using GameServices.Enums;
using GameServices.Models.Map;
using Newtonsoft.Json;

namespace GameServices.Model.Map
{
    public class Map
    {
        public List<MapTile> MapTiles { get; set; }
        public List<MapProp> MapObjects { get; set; } 
        public List<MapPlayer> MapPlayers { get; set; }

        public Map(List<MapTile> mapTiles)
        {
            MapTiles = mapTiles;
        }

        public Map(int xSize, int ySize)
        {
            var mapTiles = new List<MapTile>();
            var values = Enum.GetValues(typeof(MapTileType));
            var random = new Random();

            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    var position = new Position { X = x, Y = y };
                    var mapTileType = (MapTileType)values.GetValue(random.Next(values.Length));

                    if (x == 0 || y == 0 || x == (xSize - 1) || y == (ySize - 1))
                    {
                        mapTileType = MapTileType.Bedrock;
                    }

                    mapTiles.Add(new MapTile { Position = position, MapTileType = mapTileType });
                }
            }

            MapTiles = mapTiles;
        }

        public Map()
        {
        }

        public string AsJson()
        {
            return JsonConvert.SerializeObject(MapTiles);
        }
    }
}
