using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.RPG.Components
{
    class AttributesComponent : BaseComponent
    {
        public string Name { get; set; }

        public AttributesComponent(string aName)
        {
            Name = aName;
        }
    }
}
