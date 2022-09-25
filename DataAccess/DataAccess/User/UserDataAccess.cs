using DataAccess.Models;

namespace DataAccess.DataAccess
{
    public interface IUserDataAccess : IGenericDataAccess<UserEntity>
    {
    }

    public class UserDataAccess : GenericDataAccess<UserEntity>, IUserDataAccess
    {
        public UserDataAccess(Context context) : base(context)
        {
        }
    }
}
