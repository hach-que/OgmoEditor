using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.Protogame
{
    [Serializable]
    internal struct _InternalAppDomainData
    {
        public bool Loaded;
        public bool ProvidesRunner;
        public string Test;
    }
}
