using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Protogame
{
    /// <summary>
    /// Provides functionality for talking to the Ogmo Editor's Protogame plugin.
    /// </summary>
    public static class OgmoConnect
    {
        public static FocusableObject FocusedObject
        {
            set;
            get;
        }
    }
}
