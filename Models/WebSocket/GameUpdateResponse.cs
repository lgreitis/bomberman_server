namespace Models.WebSocket
{
    public class GameUpdateResponse
    {
        public List<Behaviour.GameOld> Games { get; set; } = new List<Behaviour.GameOld>();
    }
}
