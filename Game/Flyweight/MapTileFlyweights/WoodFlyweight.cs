using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class WoodFlyweight : MapTileFlyweight
	{
		public WoodFlyweight()
		{
			Multiplier = 0.00M;
		}

        public override decimal GetMoveAmount(decimal baseAmount)
        {
			return baseAmount * Multiplier;
        }
	}
}

