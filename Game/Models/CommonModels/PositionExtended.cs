namespace GameServices.Models.CommonModels
{
    public class PositionExtended
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public PositionExtended(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
    }
}
