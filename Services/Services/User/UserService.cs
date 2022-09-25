using DataAccess.DataAccess;
using DataAccess.Models;

namespace Services.Services
{
    public interface IUserService : IGenericService<UserEntity>
    {
        bool RegisterUser(string username, string password);
        string? Authenticate(string username, string password);
    }

    public class UserService : GenericService<UserEntity>, IUserService
    {
        public UserService(IUserDataAccess repository) : base(repository)
        {
        }

        public string? Authenticate(string username, string password)
        {
            var user = Get(x => x.Username == username);

            if (user != null && user.Password == password)
            {
                user.LoginToken = Guid.NewGuid().ToString();

                Update(user);

                return user.LoginToken;
            }

            return null;
        }

        public bool RegisterUser(string username, string password)
        {
            var userExists = GetQueryable(x => x.Username == username).Any();

            if (userExists)
            {
                return false;
            }

            var newUser = new UserEntity
            {
                Username = username,
                Password = password
            };

            Add(newUser);

            return true;
        }
    }
}
