using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Chicken_Xyfer.RPG.Components;

namespace Chicken_Xyfer.RPG.Entity
{
    class Player : Character
    {
        private ExpComponent expComponent;

        public IUser User { get; set; }

        public Player(
            string aName, 
            IUser aUser, 
            int aDmg = AttackComponent.DE_DMG, 
            int aDmgRange = AttackComponent.DE_DMGRANGE, 
            int aHp = HealthComponent.DE_HP, 
            int aDef = HealthComponent.DE_DEF,
            int aExp = ExpComponent.DE_EXP) 
            : base(aName, aDmg, aDmgRange, aHp, aDef)
        {
            User = aUser;
            expComponent = new ExpComponent(aExp);
        }

        public static T GetByUserInList<T>(IUser user, IList<T> list)
        {
            foreach (dynamic entity in list)
            {
                if (entity.User == user)
                {
                    return entity;
                }
            }
            return default;
        }
    }
}
