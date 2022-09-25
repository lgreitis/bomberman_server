namespace Models.Game
{
    public class Player
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int LocationX { get; set; } = 0;
        public int LocationY { get; set; } = 0;
    }
}
