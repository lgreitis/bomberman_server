using DataAccess.Models;

namespace DataAccess.DataAccess
{
    public interface ILobbyDataAccess : IGenericDataAccess<LobbyEntity>
    {
    }

    public class LobbyDataAccess : GenericDataAccess<LobbyEntity>, ILobbyDataAccess
    {
        public LobbyDataAccess(Context context) : base(context)
        {
        }
    }
}
