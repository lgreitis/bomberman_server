namespace Models.WebSocket.Response
{
    public class WebSocketResponse
    {
        public string ResponseId { get; set; } = string.Empty;
        public object? Data { get; set; }
    }
}
