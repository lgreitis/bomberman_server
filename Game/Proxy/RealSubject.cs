using GameServices.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Proxy
{
    public class RealSubject : Subject
    {
        public override void Connect(int lobbyId, string username, int userId, string loginToken, string ID)
        {
            GamesManager.Instance.RegisterClient(lobbyId, username, userId, loginToken, ID);
        }
    }
}
