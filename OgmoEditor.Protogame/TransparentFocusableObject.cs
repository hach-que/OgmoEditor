using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace OgmoEditor.Protogame
{
    public class TransparentFocusableObject : FocusableObject
    {
        private Type m_TopType = null;

        public TransparentFocusableObject()
        {
            this.m_TopType = this.GetType();
        }

        public override int PropertyCount()
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Length;
        }

        public override bool PropertyHasGetter(int index)
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].CanRead;
        }

        public override bool PropertyHasSetter(int index)
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].CanWrite;
        }

        public override object PropertyGet(int index)
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].GetValue(this, null);
        }
        
        public override void PropertySet(int index, object value)
        {
            this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].SetValue(this, value, null);
        }

        public override Type PropertyType(int index)
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].PropertyType;
        }

        public override string PropertyName(int index)
        {
            return this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index].Name;
        }

        public override string PropertyDescription(int index)
        {
            PropertyInfo pi = this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index];
            if (pi.GetCustomAttributes(true).Count(o => o.GetType() == typeof(DescriptionAttribute)) == 0)
                return "";
            else
                return ((DescriptionAttribute)pi.GetCustomAttributes(true).First(o => o.GetType() == typeof(DescriptionAttribute))).Description;
        }

        public override string PropertyCategory(int index)
        {
            PropertyInfo pi = this.m_TopType.GetProperties(BindingFlags.Public | BindingFlags.Instance)[index];
            if (pi.GetCustomAttributes(true).Count(o => o.GetType() == typeof(CategoryAttribute)) == 0)
                return "";
            else
                return ((CategoryAttribute)pi.GetCustomAttributes(true).First(o => o.GetType() == typeof(CategoryAttribute))).Category;
        }
    }
}
