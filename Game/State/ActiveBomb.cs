using GameServices.Interfaces;
using GameServices.Models.BombModels;
using GameServices.Models.MapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameServices.State
{
    public class ActiveBomb : BombState
    {
        public override void Handle(Bomb bomb, MapPlayer player)
        {
            if (bomb.PlacedPosition == null && bomb.ActivatableAfter == null)
            {
                bomb.State = new TakenBomb();
                player.Client.ChatParticipant.Send("took bomb");
                return;
            }

            bomb.State = new ExplodedBomb();
            player.Client.ChatParticipant.Send("exploded bomb");
        }
    }
}
