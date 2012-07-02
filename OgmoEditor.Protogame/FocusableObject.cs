using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace OgmoEditor.Protogame
{
    public abstract class FocusableObject : MarshalByRefObject
    {
        public abstract int PropertyCount();
        public abstract bool PropertyHasGetter(int index);
        public abstract bool PropertyHasSetter(int index);
        public abstract object PropertyGet(int index);
        public abstract void PropertySet(int index, object value);
        public abstract Type PropertyType(int index);
        public abstract string PropertyName(int index);
        public abstract string PropertyDescription(int index);
        public abstract string PropertyCategory(int index);
    }
}
