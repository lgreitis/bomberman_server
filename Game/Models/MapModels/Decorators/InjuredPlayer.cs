namespace GameServices.Models.MapModels.Decorators
{
    public class InjuredPlayer : Damage
    {
        public InjuredPlayer(MapPlayer player) : base(player)
        {
        }

        public override int GetHealth()
        {
            return 1;
        }
    }
}
