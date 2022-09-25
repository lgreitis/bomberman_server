namespace Models.WebSocket.Request
{
    public class WebSocketRequest
    {
        public string CommandId { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
