namespace Models.Behaviour.Game
{
    public class Client
    {
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsConnected { get; set; }
        public string SessionId { get; set; }
        public decimal X { get; set; } = 10.00M;
        public decimal Y { get; set; } = 10.00M;
    }
}
