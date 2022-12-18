using GameServices.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Proxy
{
    public class Proxy : Subject
    {
        private static Proxy? _instance = null;
        private static object _lock = new object();
        private RealSubject realSubject = new();
        private Dictionary<string, List<int>> blocked = new();

        public static Proxy Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Proxy();
                    }

                    return _instance;
                }
            }
        }

        public override void Connect(int lobbyId, string username, int userId, string loginToken, string ID)
        {
            if (blocked.ContainsKey(username) && blocked[username].Contains(lobbyId))
            {
                throw new BlockedException();
            }

            realSubject.Connect(lobbyId, username, userId, loginToken, ID);
        }

        public void Block(string username, int lobbyId)
        {
            if (blocked.ContainsKey(username))
            {
                blocked[username].Add(lobbyId);
                return;
            }

            blocked.Add(username, new List<int> { lobbyId });
        }
    }
}
