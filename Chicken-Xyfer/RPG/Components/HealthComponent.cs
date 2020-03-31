using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.RPG.Components
{
    class HealthComponent : BaseComponent
    {
        private int _hp;
        public int Hp 
        { 
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
                if (value <= 0)
                {
                    IsDead = true;
                }
                else
                {
                    IsDead = false;
                }
            }
        }

        public int Def { get; set; }

        public bool IsDead { get; private set; }

        public const int 
            DE_HP = 50, 
            DE_DEF = 0;

        public int Damaged(int aDmg)
        {
            int dmg = aDmg - Def;
            Hp -= dmg;
            return dmg;
        }

        public HealthComponent(int aHp = DE_HP, int aDef = DE_DEF)
        {
            Hp = aHp;
            Def = aDef;
        }
    }
}
