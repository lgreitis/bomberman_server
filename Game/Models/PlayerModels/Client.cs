using GameServices.Interfaces;

namespace GameServices.Models.PlayerModels
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
        public IMapTile mapTile { get; set; }
        public IBomb bomb { get; set; }
        public decimal GetSpeed(decimal baseSpeed)
        {
            return mapTile.Speed(baseSpeed);
        }
    }
}
