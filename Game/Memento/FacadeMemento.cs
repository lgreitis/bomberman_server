using System;
using GameServices.Facade;
using GameServices.Facade.Subsystems;
using GameServices.Interfaces;
using GameServices.Models.Containers;
using GameServices.Models.MapModels;

namespace GameServices.Memento
{
    public class FacadeMemento
    {
        private List<MapPlayer> mapPlayers;
        private List<IMapProp> mapProps;
        private List<MapTile> mapTiles;
        private List<MapTexture> mapTextures;

        public FacadeMemento(List<MapPlayer> mapPlayers, List<IMapProp> mapProps, List<MapTile> mapTiles, List<MapTexture> mapTextures)
        {
            this.mapPlayers = mapPlayers.Select(x => (MapPlayer)x.Clone()).ToList();
            this.mapProps = mapProps.Select(x => (IMapProp)x.Clone()).ToList();
            this.mapTiles = mapTiles.Select(x => (MapTile)x.Clone()).ToList();
            this.mapTextures = mapTextures.Select(x => (MapTexture)x.Clone()).ToList();
        }

        public void Restore(MapFacade mapFacade)
        {
            mapFacade.SetElement(mapPlayers);
            mapFacade.SetElement(mapProps);
            mapFacade.SetElement(mapTiles);
            mapFacade.SetElement(mapTextures);
        }
    }
}

