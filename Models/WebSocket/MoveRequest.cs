using Models.WebSocket.Request;

namespace Models.WebSocket
{
    public class MoveRequest : IRequestValidation
    {
        public string Token { get; set; } = string.Empty;
        public bool PositiveX { get; set; }
        public bool NegativeX { get; set; }
        public bool PositiveY { get; set; }
        public bool NegativeY { get; set; }

        public bool IsModelValid()
        {
            return !string.IsNullOrEmpty(Token);
        }
    }
}
