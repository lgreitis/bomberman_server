using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Proxy
{
    public abstract class Subject
    {
        public abstract void Connect(int lobbyId, string username, int userId, string loginToken, string ID);
    }
}
