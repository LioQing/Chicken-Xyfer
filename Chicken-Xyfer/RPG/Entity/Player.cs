using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Chicken_Xyfer.RPG.Components;
using Chicken_Xyfer.RPG.MainGame;

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

        public int GetValueByMsg(string msg)
        {
            if (msg == Data.playerInfoList[0] || msg == Data.playerInfoList[1])
            {
                return GetComponent<AttackComponent>().Dmg;
            }
            else if (msg == Data.playerInfoList[2] || msg == Data.playerInfoList[3])
            {
                return GetComponent<AttackComponent>().DmgRange;
            }
            else if (msg == Data.playerInfoList[4] || msg == Data.playerInfoList[5])
            {
                return GetComponent<HealthComponent>().Hp;
            }
            else if (msg == Data.playerInfoList[6] || msg == Data.playerInfoList[7])
            {
                return GetComponent<HealthComponent>().Def;
            }
            else if (msg == Data.playerInfoList[8] || msg == Data.playerInfoList[9])
            {
                return GetComponent<ExpComponent>().Exp;
            }
            else if (msg == Data.playerInfoList[10] || msg == Data.playerInfoList[11])
            {
                return GetComponent<ExpComponent>().Lvl;
            }
            else
            {
                return -1;
            }
        }

        public int SetValueByMsg(string msg, int value)
        {
            if (msg == Data.playerInfoList[0] || msg == Data.playerInfoList[1])
            {
                GetComponent<AttackComponent>().Dmg = value;
                return value;
            }
            else if (msg == Data.playerInfoList[2] || msg == Data.playerInfoList[3])
            {
                GetComponent<AttackComponent>().DmgRange = value;
                return value;
            }
            else if (msg == Data.playerInfoList[4] || msg == Data.playerInfoList[5])
            {
                GetComponent<HealthComponent>().Hp = value;
                return value;
            }
            else if (msg == Data.playerInfoList[6] || msg == Data.playerInfoList[7])
            {
                GetComponent<HealthComponent>().Def = value;
                return value;
            }
            else if (msg == Data.playerInfoList[8] || msg == Data.playerInfoList[9])
            {
                return GetComponent<ExpComponent>().SetExp(value);
            }
            return -1;
        }

        public static int GetInfo(IUser user, IList<Player> players, string[] lowArgs, int argPos)
        {
            if (lowArgs.Length > argPos + 1)
            {
                if (int.TryParse(lowArgs[argPos + 1], out _))
                {
                    return Int32.Parse(lowArgs[argPos + 1]);
                }
            }
            return Player.GetByUserInList(user, players).GetValueByMsg(lowArgs[argPos]);
        }
    }
}
