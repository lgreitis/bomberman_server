using Models.Game;

namespace Models.Behaviour
{
    public class Game
    {
        public int LobbyId { get; set; }
        public List<int> ValidUserIds { get; set; } = new List<int>();
        public List<Player> Players { get; set; } = new List<Player>();
    }
}