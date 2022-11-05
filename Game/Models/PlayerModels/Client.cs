namespace GameServices.Models.PlayerModels
{
    public class Client
    {
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsConnected { get; set; }
        public string SessionId { get; set; } = string.Empty;
    }
}
