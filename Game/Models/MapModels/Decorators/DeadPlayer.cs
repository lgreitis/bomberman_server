namespace GameServices.Models.MapModels.Decorators
{
    public class DeadPlayer : Damage
    {
        private MapPlayer _player;
        public DeadPlayer(MapPlayer player)
        {
            _player = player;
        }

        public override int GetHealth()
        {
            return 0;
        }
    }
}
