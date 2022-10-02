using DataAccess.Models;

namespace DataAccess.DataAccess
{
    public interface ILobbyUserDataAccess : IGenericDataAccess<LobbyUserEntity>
    {
    }

    public class LobbyUserDataAccess : GenericDataAccess<LobbyUserEntity>, ILobbyUserDataAccess
    {
        public LobbyUserDataAccess(Context context) : base(context)
        {
        }
    }
}
