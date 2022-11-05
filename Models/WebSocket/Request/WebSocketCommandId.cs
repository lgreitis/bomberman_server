namespace Models.WebSocket.Request
{
    public static class WebSocketCommandId
    {
        public const string Connect = "CONNECT";
        public const string Move = "MOVE";
        public const string JoinLobby = "JOIN_LOBBY";
        public const string PlaceBomb = "PLACE_BOMB";

        public static readonly List<string> AllCommands = new List<string>()
        {
            JoinLobby,
            Connect,
            Move,
            PlaceBomb
        };
    }
}
