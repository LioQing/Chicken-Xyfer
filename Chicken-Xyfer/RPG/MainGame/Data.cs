using Chicken_Xyfer.RPG.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.RPG.MainGame
{
    class Data
    {
        public static IList<string> playerInfoList = new List<string>()
        {
            "damage", "dmg",
            "damage_range", "dmgrnge",
            "health", "hp",
            "defence", "def",
            "experience", "exp",
            "level", "lvl"
        };

        public static IList<Player> players = new List<Player>();
        public static IList<Monster> monsters = new List<Monster>();
    }
}
