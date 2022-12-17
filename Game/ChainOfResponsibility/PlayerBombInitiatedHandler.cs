using System;
using GameServices.Models.BombModels;
using GameServices.Models.MapModels;

namespace GameServices.ChainOfResponsibility
{
	public class PlayerBombInitiatedHandler : Handler
	{
		MapPlayer _mapPlayer;

		public PlayerBombInitiatedHandler(MapPlayer player)
		{
			_mapPlayer = player;
		}

		public override void HandleRequest()
		{
            _mapPlayer.HasProp = false;
            _mapPlayer.SetBomb(new RegularBomb());
            _mapPlayer.GetBomb().Reset();

            if (_nextHandler != null)
			{
				_nextHandler.HandleRequest();
			}
        }
    }
}

