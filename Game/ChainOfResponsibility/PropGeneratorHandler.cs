using System;
using GameServices.Models.CommonModels;
using GameServices.Models.ManagerModels;
using GameServices.Models.MapModels.MapProps;

namespace GameServices.ChainOfResponsibility
{
	public class PropGeneratorHandler : Handler
	{
		private GameManager _gameManager;

		public PropGeneratorHandler(GameManager gameManager)
		{
			_gameManager = gameManager;
        }

        public override void HandleRequest()
        {
            var random = new Random();
            var newProp = new CircularBombProp { Position = new Position(random.Next(1, 31), random.Next(1, 23)) };
            _gameManager.AddProp(newProp);

            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest();
            }
        }
    }
}

