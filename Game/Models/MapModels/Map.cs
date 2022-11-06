using GameServices.Interfaces;
using Newtonsoft.Json;

namespace GameServices.Models.MapModels
{
    public class Map
    {
        public List<IMapTile> MapTiles { get; set; }
        public List<IMapProp> MapProps { get; set; } 
        public List<MapPlayer> MapPlayers { get; set; }
        public List<MapTexture> MapTextures { get; set; } = new List<MapTexture>();

        public Map()
        {
        }

        public string MapTilesAsJson()
        {
            return JsonConvert.SerializeObject(MapTiles);
        }

        public string MapPlayersAsJson()
        {
            return JsonConvert.SerializeObject(MapPlayers);
        }
    }
}
