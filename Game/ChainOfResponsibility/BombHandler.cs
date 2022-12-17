using System;
using GameServices.Enums;
using GameServices.Iterator;
using GameServices.Models.CommonModels;
using GameServices.Models.ManagerModels;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;

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
            var aggregate = new BombExplosionTemplateAggregate();
            aggregate.Set(_gameManager.Map.GetBombExplosionTemplates());

            var iterator = aggregate.CreateIterator();
            var item = iterator.First() as BombExplosionTemplate;

            while (item != null)
            {
                item.DoBombExplosion(_affectedPositions);

                if (!iterator.IsDone())
                {
                    item = iterator.Next() as BombExplosionTemplate;
                }
            }

            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest();
            }
        }
    }
}

