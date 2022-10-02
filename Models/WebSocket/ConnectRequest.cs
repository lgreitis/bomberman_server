using Models.WebSocket.Request;

namespace Models.WebSocket
{
    public class ConnectRequest : IRequestValidation
    {
        public int LobbyId { get; set; }
        public string Token { get; set; } = string.Empty;

        public bool IsModelValid()
        {
            return !string.IsNullOrEmpty(Token)
                   && LobbyId > 0;
        }
    }
}
