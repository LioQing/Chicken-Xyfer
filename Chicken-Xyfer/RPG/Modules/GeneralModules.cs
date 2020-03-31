using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken_Xyfer.RPG.Entity;
using Chicken_Xyfer.RPG.Components;
using Discord;
using Chicken_Xyfer.RPG.MainGame;

namespace Chicken_Xyfer.RPG.Modules
{
    class GeneralModules
    {
        public static string GetCharacterNames<T>(IList<T> characters)
        {
            string characterStr = "";

            foreach (dynamic character in characters)
            {
                if (!character.GetComponent<HealthComponent>().IsDead)
                {
                    int lvl;

                    if (typeof(T) == typeof(Monster))
                    {
                        lvl = character.GetComponent<MExpComponent>().Lvl;
                    }
                    else
                    {
                        lvl = character.GetComponent<ExpComponent>().Lvl;
                    }

                    characterStr += character.GetComponent<AttributesComponent>().Name + $" [Lvl. {lvl}]({character.GetComponent<HealthComponent>().Hp} HP)\n";
                }
            }

            if (characterStr.Length > 0)
            {
                characterStr = characterStr.Substring(0, characterStr.Length - 1);
            }

            return characterStr;
        }

        public static string GetOfficialInfoNameByMsg(string msg)
        {
            if (msg == Data.playerInfoList[0] || msg == Data.playerInfoList[1])
            {
                return "Damage";
            }
            else if (msg == Data.playerInfoList[2] || msg == Data.playerInfoList[3])
            {
                return "Damage Range";
            }
            else if (msg == Data.playerInfoList[4] || msg == Data.playerInfoList[5])
            {
                return "HP";
            }
            else if (msg == Data.playerInfoList[6] || msg == Data.playerInfoList[7])
            {
                return "Defence";
            }
            else if (msg == Data.playerInfoList[8] || msg == Data.playerInfoList[9])
            {
                return "Exp";
            }
            else if (msg == Data.playerInfoList[10] || msg == Data.playerInfoList[11])
            {
                return "Level";
            }
            else
            {
                return "";
            }
        }

        public static int GetPlayerInfo(IUser user, IList<Player> players, string[] lowArgs, int argPos)
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

        public static void LoadData(out IList<Player> players, out IList<Monster> monsters)
        {
            players = Data.players;
            monsters = Data.monsters;
        }
    }
}
