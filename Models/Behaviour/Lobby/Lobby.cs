namespace Models.Behaviour.Lobby
{
    public class Lobby
    {
        public int LobbyId { get; set; }
        public List<int> UserIds { get; set; } = new List<int>();
    }
}
