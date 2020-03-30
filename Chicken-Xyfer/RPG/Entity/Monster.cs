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
