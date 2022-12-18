using System;
using GameServices.Enums;
using GameServices.Flyweight.MapTileFlyweights;

namespace GameServices.Flyweight
{
	public class MapTileFlyweightFactory
	{
		private static Dictionary<MapTileType, MapTileFlyweight> MapTiles = new Dictionary<MapTileType, MapTileFlyweight>();

		public static MapTileFlyweight Get(MapTileType mapTileType)
		{
			if (MapTiles.ContainsKey(mapTileType))
			{
				return MapTiles[mapTileType];
			}

			MapTileFlyweight flyweight = null;

			switch (mapTileType)
            {
                case MapTileType.Bedrock:
                    flyweight = new BedrockFlyweight();
                    break;
                case MapTileType.Cobblestone:
                    flyweight = new CobblestoneFlyweight();
                    break;
                case MapTileType.Grass:
                    flyweight = new GrassFlyweight();
                    break;
                case MapTileType.Ice:
                    flyweight = new IceFlyweight();
                    break;
                case MapTileType.Sand:
                    flyweight = new SandFlyweight();
                    break;
                case MapTileType.Water:
                    flyweight = new WaterFlyweight();
                    break;
                case MapTileType.Wood:
                    flyweight = new WoodFlyweight();
                    break;
            }

			MapTiles.Add(mapTileType, flyweight);

			return flyweight;
		}
	}
}

