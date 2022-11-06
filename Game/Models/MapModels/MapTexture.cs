using GameServices.Enums;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels
{
    public class MapTexture
    {
        public TextureType TextureType { get; set; }
        public Position Position { get; set; }
        public DateTime? ValidUntil { get; set; }
        public int? TimeLeft
        {
            get
            {
                if (!ValidUntil.HasValue)
                {
                    return null;
                }

                if (DateTime.Now > ValidUntil.Value)
                {
                    return 0;
                }

                return (ValidUntil.Value - DateTime.Now).Milliseconds;
            }
        }
    }
}
