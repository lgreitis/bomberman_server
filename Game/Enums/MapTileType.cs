namespace GameServices.Enums
{
    public enum MapTileType
    {
        Grass = 1,
        Cobblestone = 2,
        Wood = 3,
        Bedrock = 4
    }

    public static class MapTileTypeExtensions
    {
        public static bool IsWalkable(this MapTileType mapTileType)
        {
            return !(mapTileType == MapTileType.Bedrock
                     || mapTileType == MapTileType.Cobblestone);
        }
    }
}
