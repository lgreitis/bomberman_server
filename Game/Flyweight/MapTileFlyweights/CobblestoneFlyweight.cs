using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class CobblestoneFlyweight : MapTileFlyweight
	{
		public CobblestoneFlyweight()
        {
            Multiplier = 0.00M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

