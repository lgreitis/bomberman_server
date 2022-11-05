using GameServices.Models.CommonModels;
using GameServices.Models.PlayerModels;
using GameServices.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Command
{
    public class PlaceBombCommand : ICommand
    {
        private Client _client;
        public PlaceBombCommand(Client client)
        {
            _client = client;
        }

        public void Execute()
        {
            _client.bomb.Place(new Position((int)_client.X, (int)_client.Y));
        }
    }
}
