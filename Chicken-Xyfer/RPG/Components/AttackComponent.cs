using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.RPG.Components
{
    public class AttackComponent : BaseComponent
    {
        Random rand = new Random();

        public int Dmg { get; set; }
        public int DmgRange { get; set; }

        public const int 
            DE_DMG = 10, 
            DE_DMGRANGE = 5;

        public int Attack()
        {
            return Dmg + rand.Next(-DmgRange, DmgRange);
        }

        public AttackComponent(int aDmg = DE_DMG, int aDmgRange = DE_DMGRANGE)
        {
            Dmg = aDmg;
            DmgRange = aDmgRange;
        }
    }
}
