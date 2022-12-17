using System;
using GameServices.Models.MapModels;

namespace GameServices.ChainOfResponsibility
{
	public class PlayerBombExplodedHandler : Handler
	{
        MapPlayer _mapPlayer;

		public PlayerBombExplodedHandler(MapPlayer mapPlayer)
		{
            _mapPlayer = mapPlayer;
		}

        public override void HandleRequest()
        {
            _mapPlayer.RemoveBombState();

            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest();
            }
        }
    }
}

