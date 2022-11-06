namespace Models.WebSocket.Request
{
    public static class WebSocketCommandId
    {
        public const string Connect = "CONNECT";
        public const string Move = "MOVE";
        public const string JoinLobby = "JOIN_LOBBY";
        public const string UseBomb = "USE_BOMB";
        public const string UndoBomb = "UNDO_BOMB";

        public static readonly List<string> AllCommands = new List<string>()
        {
            JoinLobby,
            Connect,
            Move,
            UseBomb,
            UndoBomb
        };
    }
}
