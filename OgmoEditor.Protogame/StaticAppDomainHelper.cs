using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace OgmoEditor.Protogame
{
    public static class StaticAppDomainHelper
    {
        public static AppDomain CreateAppDomain(string name, string pluginPath)
        {
            AppDomainSetup setup = new AppDomainSetup();
            setup.PrivateBinPath = pluginPath;
            AppDomain domain = AppDomain.CreateDomain(name, null, setup);
            return domain;
        }

        public static bool Dummy;
    }
}
