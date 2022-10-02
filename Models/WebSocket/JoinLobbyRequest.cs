using Models.WebSocket.Request;

namespace Models.WebSocket
{
    public class JoinLobbyRequest : IRequestValidation
    {
        public int LobbyId { get; set; }
        public string Token { get; set; } = string.Empty;

        public bool IsModelValid()
        {
            return !string.IsNullOrWhiteSpace(Token)
                   && LobbyId > 0;
        }
    }
}
