namespace GameServices.Models.MapModels.Decorators
{
    public class DeadPlayer : Damage
    {
        public DeadPlayer(MapPlayer player) : base(player)
        {
        }

        public override int GetHealth()
        {
            return 0;
        }
    }
}
