using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken_Xyfer.RPG.Components;

namespace Chicken_Xyfer.RPG.Entity
{
    class Monster : Character
    {
        MExpComponent mExpComponent;

        public IDictionary<Player, int> Attackers = new Dictionary<Player, int>();

        public int AttackedByPlayer(int aDmg, Player player)
        {
            int value = GetComponent<HealthComponent>().Damaged(aDmg);
            
            if (Attackers.ContainsKey(player))
            {
                Attackers[player] += value;
            }
            else
            {
                Attackers.Add(player, value);
            }
            return value;
        }

        public IDictionary<Player, int[]> DropExp(IList<Player> players)
        {
            IDictionary<Player, int[]> returnDict = new Dictionary<Player, int[]>();
            int MaxDmg = Attackers.Values.Max();

            foreach (Player player in players)
            {
                if (Attackers.ContainsKey(player))
                {
                    int expGained = (int)(((double)Attackers[player] / (double)MaxDmg) * GetComponent<MExpComponent>().Exp);
                    int lvlUp = player.GetComponent<ExpComponent>().GainExp(expGained);

                    returnDict.Add(player, new int[] { expGained, lvlUp });
                }
            }

            return returnDict;
        }

        public Monster(
            string aName, 
            int aDmg = AttackComponent.DE_DMG, 
            int aDmgRange = AttackComponent.DE_DMGRANGE, 
            int aHp = HealthComponent.DE_HP,
            int aDef = HealthComponent.DE_DEF,
            int aLvl = MExpComponent.DE_LVL) 
            : base(aName, aDmg, aDmgRange, aHp, aDef)
        {
            mExpComponent = new MExpComponent(aLvl);
        }
    }
}
