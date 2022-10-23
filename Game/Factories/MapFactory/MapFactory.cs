using GameServices.Enums;

namespace GameServices.Factories.MapFactory
{
    public class MapFactory
    {
        public static MapAbstractFactory GetMapFactory(Level level)
        {
            switch (level)
            {
                case Level.First:
                    return new FirstLevelMapFactory();
                case Level.Second:
                    return new SecondLevelMapFactory();
                case Level.Third:
                    return new ThirdLevelMapFactory();
                default:
                    return new FirstLevelMapFactory();
            }
        }
    }
}
