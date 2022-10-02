namespace Models.WebSocket
{
    public class GameUpdateResponse
    {
        public List<Behaviour.Game> Games { get; set; } = new List<Behaviour.Game>();
    }
}
