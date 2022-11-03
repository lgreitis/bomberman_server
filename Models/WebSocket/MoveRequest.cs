using Models.WebSocket.Request;

namespace Models.WebSocket
{
    public class MoveRequest : IRequestValidation
    {
        public bool PositiveX { get; set; }
        public bool NegativeX { get; set; }
        public bool PositiveY { get; set; }
        public bool NegativeY { get; set; }

        public bool IsModelValid()
        {
            return true;
        }
    }
}
