using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OgmoEditor.Protogame
{
    public class DataProxy : MarshalByRefObject, IDataProxy
    {
        internal static _InternalAppDomainData _AppDomainData;
        private Type OgmoConnectCachedType = null;

        public bool Loaded
        {
            get
            {
                return _AppDomainData.Loaded;
            }
        }

        public bool ProvidesRunner
        {
            get
            {
                return _AppDomainData.ProvidesRunner;
            }
        }

        private void UpdateCachedConnector()
        {
            try
            {
                Assembly a = AppDomain.CurrentDomain.GetAssemblies().First(v => v.FullName.StartsWith("OgmoEditor.Protogame,"));
                this.OgmoConnectCachedType = a.GetTypes().First(v => v.FullName == "OgmoEditor.Protogame.OgmoConnect");
            }
            catch (Exception)
            {
                this.OgmoConnectCachedType = null;
            }
        }

        public FocusableObject FocusedObject
        {
            get
            {
                // We need to get this dynamically.
                if (this.OgmoConnectCachedType == null)
                    this.UpdateCachedConnector();
                if (this.OgmoConnectCachedType != null)
                    return (FocusableObject)this.OgmoConnectCachedType.GetProperty("FocusedObject", BindingFlags.Static | BindingFlags.Public).GetValue(null, null);
                else
                    return null;
            }
            set
            {
                // We need to set this dynamically.
                if (this.OgmoConnectCachedType == null)
                    this.UpdateCachedConnector();
                if (this.OgmoConnectCachedType != null)
                    this.OgmoConnectCachedType.GetProperty("FocusedObject", BindingFlags.Static | BindingFlags.Public).SetValue(null, value, null);
            }
        }
    }

    public interface IDataProxy
    {
        bool Loaded
        {
            get;
        }

        bool ProvidesRunner
        {
            get;
        }

        FocusableObject FocusedObject
        {
            get;
            set;
        }
    }
}
