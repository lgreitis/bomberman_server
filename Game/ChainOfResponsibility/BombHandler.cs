using System;
using GameServices.Enums;
using GameServices.Models.CommonModels;
using GameServices.Models.ManagerModels;
using GameServices.Models.MapModels;

namespace GameServices.ChainOfResponsibility
{
    public class BombHandler : Handler
    {
        GameManager _gameManager;
        List<Position> _affectedPositions;

        public BombHandler(GameManager gameManager, List<Position> affectedPositions)
        {
            _gameManager = gameManager;
            _affectedPositions = affectedPositions;
        }

        public override void HandleRequest()
        {
            var bombExplosionActions = _gameManager.Map.GetBombExplosionTemplates();

            foreach (var action in bombExplosionActions)
            {
                action.DoBombExplosion(_affectedPositions);
            }

            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest();
            }
        }
    }
}

