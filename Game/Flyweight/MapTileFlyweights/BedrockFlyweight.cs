using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class BedrockFlyweight : MapTileFlyweight
	{
		public BedrockFlyweight()
        {
            Multiplier = 0.00M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

