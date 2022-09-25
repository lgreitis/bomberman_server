using Models.Game;

namespace Models.WebSocket
{
    public class GameUpdateResponse
    {
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
