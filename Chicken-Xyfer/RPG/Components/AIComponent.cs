using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken_Xyfer.RPG.Entity;

namespace Chicken_Xyfer.RPG.Components
{
    class AIComponent
    {
        Random rand = new Random();

        public Player TargetSelect(IList<Player> players)
        {
            return players[rand.Next(players.Count)];
        }
    }
}
