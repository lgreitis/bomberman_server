using Models.Behaviour.Lobby;

namespace GameServer.Data
{
    public abstract class Subject
    {
        public List<LobbyUser> LobbyUsers { get; set; } = new List<LobbyUser>();
        public abstract void Send();
        public void Subscribe(LobbyUser lobbyUser)
        {
            LobbyUsers.Add(lobbyUser);
        }

        public void Unsubscribe(LobbyUser lobbyUser)
        {
            LobbyUsers.Remove(LobbyUsers.Single(x => x.Connection.Equals(lobbyUser.Connection)));
        }
    }
}
