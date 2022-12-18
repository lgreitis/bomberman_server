using System;
using GameServices.Models.CommonModels;
using GameServices.TemplateMethod;

namespace GameServices.Visitor
{
    public class BombExplosionVisitor : IVisitor
    {
        private List<Position> AffectedPositions { get; set; }

        public BombExplosionVisitor(List<Position> affectedPositions)
        {
            AffectedPositions = affectedPositions;
        }

        public void VisitElement(IElement element)
        {
            var typedElement = element as BombExplosionTemplate;

            if (typedElement != null)
            {
                typedElement.DoBombExplosion(AffectedPositions);
            }
        }
    }
}

