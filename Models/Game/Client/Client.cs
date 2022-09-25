namespace Models.Game.Client
{
    public class Client
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public bool WebSocketLogin { get; set; }
        public int PositionX 
        { 
            get
            {
                return new Random().Next(100, 700);
            }
        }
        public int PositionY
        {
            get
            {
                return new Random().Next(100, 700);
            }
        }
    }
}
