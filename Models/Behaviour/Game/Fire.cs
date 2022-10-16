namespace Models.Game
{
    public class Fire : IItem
    {
        private readonly int radius = 3;

        public int GetRadius()
        {
            return radius;
        }
    }
}
