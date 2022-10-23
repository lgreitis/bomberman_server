using GameServices.Builders;
using GameServices.Enums;
using GameServices.Factories.MapFactory;
using GameServices.Models.MapModels;
using Models.Behaviour.Game;
using Newtonsoft.Json;

namespace GameServices.Models.ManagerModels
{
    public class GameManager
    {
        public int LobbyId { get; set; }
        public Map? Map { get; set; }
        public Level Level { get; set; } = Level.First;
        public List<Client> Clients { get; set; } = new List<Client>();

        public void InitializeLevel()
        {
            var director = new MapDirector
            {
                Builder = new MapBuilder(MapFactory.GetMapFactory(Level))
            };

            Map = director.BuildMap();

            switch (Level)
            {
                case Level.First:
                    Level = Level.Second;
                    break;
                case Level.Second:
                    Level = Level.Third;
                    break;
                case Level.Third:
                    Level = Level.First;
                    break;
            }
        }

        public List<string> GetSessionIds()
        {
            return Clients.Select(x => x.SessionId).ToList();
        }
    }
}
