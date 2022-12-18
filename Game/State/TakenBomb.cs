using GameServices.Interfaces;
using GameServices.Models.BombModels;
using GameServices.Models.MapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.State
{
    public class TakenBomb : BombState
    {
        public override void Handle(Bomb bomb, MapPlayer player)
        {
            bomb.State = new ActiveBomb();
            player.Client.ChatParticipant.Send("activated bomb");
        }
    }
}
