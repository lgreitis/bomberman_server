using DataAccess.DataAccess;
using DataAccess.Models;

namespace Services.Services.Lobby
{
    public interface ILobbyService : IGenericService<LobbyEntity>
    {
        int InitLobby();

        bool JoinLobby(int lobbyId, int userId);
    }
    public class LobbyService : GenericService<LobbyEntity>, ILobbyService
    {
        private readonly IUserDataAccess _userRepository;
        private readonly ILobbyUserDataAccess _lobbyUserDataAccess;

        public LobbyService(ILobbyDataAccess repository, IUserDataAccess userRepository, ILobbyUserDataAccess userLobbyRepository) : base(repository)
        {
            _userRepository = userRepository;
            _lobbyUserDataAccess = userLobbyRepository;
        }

        public int InitLobby()
        {
            var newLobby = new LobbyEntity();

            _repository.Add(newLobby);

            return newLobby.LobbyId;
        }

        public bool JoinLobby(int lobbyId, int userId)
        {
            var lobby = _repository.GetQueryable(x => x.LobbyId == lobbyId).Any();

            if (!lobby)
            {
                return false;
            }

            var user = _userRepository.GetQueryable(x => x.UserId == userId).Any();

            if (!user)
            {
                return false;
            }

            var lobbyUser = new LobbyUserEntity();
            lobbyUser.LobbyId = lobbyId;
            lobbyUser.UserId = userId;

            _lobbyUserDataAccess.Add(lobbyUser);

            return true;
        }
    }
}
