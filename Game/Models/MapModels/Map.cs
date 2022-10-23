using Newtonsoft.Json;

namespace GameServices.Models.MapModels
{
    public class Map
    {
        public List<MapTile> MapTiles { get; set; }
        public List<MapProp> MapProps { get; set; } 
        public List<MapPlayer> MapPlayers { get; set; }

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
