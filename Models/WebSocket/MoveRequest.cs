namespace Models.WebSocket
{
    public class MoveRequest
    {
        public string Token { get; set; } = string.Empty;
        public bool PositiveX { get; set; }
        public bool NegativeX { get; set; }
        public bool PositiveY { get; set; }
        public bool NegativeY { get; set; }
    }
}
