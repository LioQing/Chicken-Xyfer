using Chicken_Xyfer.RPG.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Chicken_Xyfer.Main;

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

        static string monsterJson = File.ReadAllText(Token.GetGrandParentDir() + @"\RPG\MainGame\Monsters.json");
        public static MonsterData monsterTypes = JsonConvert.DeserializeObject<MonsterData>(monsterJson);

        public static IList<Player> players = new List<Player>();
        public static IList<Monster> monsters = new List<Monster>();
    }

    public class MonsterType
    {
        public string Name { get; set; }
        public int Dmg { get; set; }
        public int DmgRange { get; set; }
        public int Hp { get; set; }
        public int Def { get; set; }
        public int Lvl { get; set; }
    }

    public class MonsterData
    {
        public IList<MonsterType> Types { get; set; }
    }
}
