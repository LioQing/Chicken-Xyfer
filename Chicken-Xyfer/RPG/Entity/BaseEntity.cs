using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Chicken_Xyfer.RPG.Components;

namespace Chicken_Xyfer.RPG.Entity
{
    class BaseEntity
    {
        private AttributesComponent attributesComponent;

        public BaseEntity(string aName)
        {
            attributesComponent = new AttributesComponent(aName);
        }

        public static T GetByNameInList<T>(string name, IList<T> list)
        {
            foreach (dynamic entity in list)
            {
                if (entity.attributesComponent.Name == name)
                {
                    return entity;
                }
            }
            return default;
        }

        public T GetComponent<T>()
        {
            Type theType = GetType();
            while (theType != typeof(BaseEntity).BaseType)
            {
                IList<FieldInfo> fields = theType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(T))
                    {
                        return (T)field.GetValue(this);
                    }
                }

                theType = theType.BaseType;
            }

            return default;
        }
    }
}
