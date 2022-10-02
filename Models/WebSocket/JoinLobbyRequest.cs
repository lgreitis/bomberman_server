namespace Models.WebSocket
{
    public class JoinLobbyRequest
    {
        public string LobbyId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
