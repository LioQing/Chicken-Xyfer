using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chicken_Xyfer.RPG.Entity;
using Chicken_Xyfer.RPG.Components;

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
                    characterStr += character.GetComponent<AttributesComponent>().Name + $"({character.GetComponent<HealthComponent>().Hp} HP)\n";
                }
            }

            if (characterStr.Length > 0)
            {
                characterStr = characterStr.Substring(0, characterStr.Length - 1);
            }

            return characterStr;
        }
    }
}
