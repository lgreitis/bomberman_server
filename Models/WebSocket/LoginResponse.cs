namespace Models.WebSocket
{
    public class LoginResponse
    {
        public string Type { get; set; }
        public bool IsSuccess { get; set; }
        public string? Username { get; set; }
    }
}
