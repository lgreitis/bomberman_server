namespace Models.REST.Lobby.Get
{
    public class GetLobbiesResponse
    {
        public class Lobby
        {
            public int LobbyId { get; set; }
            public bool IsFull { get; set; } = false;
        }
        public List<Lobby> Lobbies { get; set; } = new List<Lobby>();
    }
}
