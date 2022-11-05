namespace GameServices.Models.MapModels.Decorators
{
    public class BleedingPlayer : Damage
    {
        private MapPlayer _player;
        public BleedingPlayer(MapPlayer player)
        {
            _player = player;
        }

        public override int GetHealth()
        {
            return 2;
        }
    }
}
