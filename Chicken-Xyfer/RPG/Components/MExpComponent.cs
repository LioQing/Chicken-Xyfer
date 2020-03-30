using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.RPG.Components
{
    class MExpComponent : BaseComponent
    {
        Random rand = new Random();

        public int Lvl { get; set; }//Level formula: exp = 32 * (lvl + 1) * (rand(0.8, 1.2)) ^ 2

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
            }
        }

        public const int
            DE_LVL = 0;

        public int GenExpByLvl()
        {
            return (int)Math.Floor(32 * (Lvl + 1) * Math.Pow(rand.Next(800, 1200) * 0.001, 2));
        }

        public MExpComponent(int aLvl = DE_LVL)
        {
            Lvl = aLvl;
            Exp = GenExpByLvl();
        }
    }
}
