using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Chicken_Xyfer.RPG.Components
{
    class ExpComponent : BaseComponent
    {
        private int _lvl;
        public int Lvl //Level formula: exp = 1000 * (lvl * 0.2) ^ 2
        { 
            get
            {
                return _lvl;
            }
            private set
            {
                if (value > MAX_LVL)
                {
                    _lvl = MAX_LVL;
                }
                else
                {
                    _lvl = value;
                }
            }
        }

        private int _exp;
        public int Exp
        { 
            get
            {
                return _exp;
            }
            private set
            {
                _exp = value;

                if (Lvl < 32)
                {
                    while (Exp >= GetNextLvlExp() && Lvl != 32)
                    {
                        Lvl++;
                        Exp -= GetExpByLvl(Lvl);
                    }
                }
            }
        }

        public const int
            DE_EXP = 0;

        public const int
            MAX_LVL = 32;

        public ExpComponent(int aExp = DE_EXP)
        {
            Exp = aExp;
        }

        public static int GetLvlByExp(int exp) => (int)Math.Floor(Math.Sqrt(exp / 1000)/0.2);
        public static int GetExpByLvl(int lvl) => (int)Math.Floor(1000*Math.Pow(lvl*0.2, 2));

        public int SetExp(int value)
        {
            int temp = Lvl;
            Exp = value;
            return Lvl - temp;
        }

        public int GetNextLvlExp()
        {
            return GetExpByLvl(Lvl + 1);
        }

        public int GetRequiredExpToLvl(int lvl)
        {
            int rExp = 0;

            for (int i = Lvl; i <= lvl; ++i)
            {
                rExp += GetExpByLvl(i);
            }

            rExp -= Exp;

            return rExp;
        }
    }
}
