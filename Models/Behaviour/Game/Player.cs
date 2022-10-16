namespace Models.Game
{
    public class Player
    {
        public string Username { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsConnected { get; set; }
        public int LocationX { get; set; } = 0;
        public int LocationY { get; set; } = 0;
        public int Health { get; set; } = 3;
        public IItem Item{ get; set; }
    }
}
