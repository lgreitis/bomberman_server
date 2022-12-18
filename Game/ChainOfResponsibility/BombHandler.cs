using System;
using GameServices.Composite;
using GameServices.Enums;
using GameServices.Iterator;
using GameServices.Models.CommonModels;
using GameServices.Models.ManagerModels;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;
using GameServices.Visitor;

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
            var playerComposite = new BombExplosionComposite(_gameManager.GetPlayerContainer());
            var tileComposite = new BombExplosionComposite(_gameManager.GetTileContainer());
            var textureComposite = new BombExplosionComposite(_gameManager.GetTextureContainer());

            tileComposite.AddChild(playerComposite);
            tileComposite.AddChild(textureComposite);

            var visitor = new BombExplosionVisitor(_affectedPositions);
            tileComposite.Accept(visitor);

            if (_nextHandler != null)
            {
                _nextHandler.HandleRequest();
            }
        }
    }
}

