using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class GrassFlyweight : MapTileFlyweight
	{
		public GrassFlyweight()
        {
            Multiplier = 1.00M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

