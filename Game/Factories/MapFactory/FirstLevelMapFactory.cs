using GameServices.Model.Map;
using GameServices.Models.Map;

namespace GameServices.Factories.MapFactory
{
    public class FirstLevelMapFactory : MapAbstractFactory
    {
        public override List<MapPlayer> getPlayers()
        {
            throw new NotImplementedException();
        }

        public override List<MapProp> getProps()
        {
            throw new NotImplementedException();
        }

        public override List<MapTile> getTiles()
        {
            throw new NotImplementedException();
        }
    }
}
