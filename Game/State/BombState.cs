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
    public abstract class BombState
    {
        public abstract void Handle(Bomb bomb, MapPlayer player);
    }
}
