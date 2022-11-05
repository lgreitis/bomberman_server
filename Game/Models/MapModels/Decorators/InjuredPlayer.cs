namespace GameServices.Models.MapModels.Decorators
{
    internal class InjuredPlayer : Damage
    {
        private MapPlayer _player;
        public InjuredPlayer(MapPlayer player)
        {
            _player = player;
        }

        public override int GetHealth()
        {
            return 1;
        }
    }
}
