namespace GameServices.Enums
{
    public enum MapTileType
    {
        Grass = 1,
        Cobblestone = 2,
        Wood = 3,
        Bedrock = 4,
        Ice = 5,
        Sand = 6,
        Water = 7,
    }

    public static class MapTileTypeExtensions
    {
        public static bool IsWalkable(this MapTileType mapTileType)
        {
            return !(mapTileType == MapTileType.Bedrock
                     || mapTileType == MapTileType.Cobblestone
                     || mapTileType == MapTileType.Wood);
        }
    }
}
