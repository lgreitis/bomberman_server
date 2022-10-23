using GameServices.Models.Player;

namespace GameServices.Interfaces
{
    public interface IProp
    {
        bool IsWalkable { get; set; }
        bool IsPickable { get; set; }
        void Activate(Player player);
    }
}
