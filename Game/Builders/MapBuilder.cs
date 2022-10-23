using GameServices.Factories.MapFactory;
using GameServices.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.Builders
{
    public class MapBuilder : IMapBuilder
    {
        private Map map = new Map();
        private MapAbstractFactory factory;

        public MapBuilder(MapAbstractFactory factory)
        {
            this.factory = factory;
        }

        public void AddPlayers()
        {
            map.MapTiles = factory.getTiles();
        }

        public void AddProps()
        {
            map.MapObjects = factory.getProps();
        }

        public void AddTiles()
        {
            map.MapPlayers = factory.getPlayers();
        }

        public Map GetMap() 
        {
            Map builtMap = this.map;
            this.map = new Map();

            return builtMap;
        }
    }
}
