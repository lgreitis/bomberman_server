using GameServices.Model.Map;
using GameServices.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Factories.MapFactory
{
    public class SecondLevelMapFactory : MapAbstractFactory
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
