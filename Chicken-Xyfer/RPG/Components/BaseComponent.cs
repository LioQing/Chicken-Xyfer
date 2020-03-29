using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Chicken_Xyfer.RPG.Components
{
    public class BaseComponent
    {
        public T GetValue<T>(string variable)
        {
            return (T)GetType().GetField(variable, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        }

        public void SetValue<T>(string variable, T value)
        {
            GetType().GetField(variable, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, value);
        }
    }
}
