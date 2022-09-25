namespace Models.WebSocket.Request
{
    public static class WebSocketCommandId
    {
        public const string Connect = "CONNECT";
        public const string Move = "MOVE";

        public static readonly List<string> AllCommands = new List<string>()
        {
            Connect,
            Move
        };
    }
}
