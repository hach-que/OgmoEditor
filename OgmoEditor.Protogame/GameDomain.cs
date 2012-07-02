using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;

namespace OgmoEditor.Protogame
{
    public class GameDomain : MarshalByRefObject, IDisposable
    {
        private AppDomain m_AppDomain = null;

        public GameDomain(string path, string mode, string level)
        {
            if (!File.Exists(path))
                throw new ArgumentException("The specified path must exist.");
            this.m_AppDomain = StaticAppDomainHelper.CreateAppDomain("ProtogameExecutionDomain", Assembly.GetExecutingAssembly().FullName);
            this.m_AppDomain.DoCallBack(new CallbackHolder(path, mode, level).Execute);
        }

        [Serializable]
        private class CallbackHolder
        {
            string m_Path;
            string m_Mode;
            string m_Level;

            public CallbackHolder(string path, string mode, string level)
            {
                this.m_Path = path;
                this.m_Mode = mode;
                this.m_Level = level;
            }

            public void Execute()
            {
                InitializeAppDomain(new string[] { this.m_Path, this.m_Mode, this.m_Level });
            }
        }

        public IDataProxy Proxy
        {
            get
            {
                return (IDataProxy)this.m_AppDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().CodeBase, "OgmoEditor.Protogame.DataProxy");
            }
        }

        private static void InitializeAppDomain(string[] args)
        {
            string protogamePluginGame = args[0];
            string protogamePluginMode = args[1];
            string protogamePluginLevel = args.Length > 2 ? args[2] : null;

            // Load game assembly.
            Assembly target = Assembly.LoadFrom(protogamePluginGame);
            DirectoryInfo di = new FileInfo(protogamePluginGame).Directory;
            string olddir = Environment.CurrentDirectory;

            try
            {
                Environment.CurrentDirectory = di.FullName;

                // Check the mode.
                switch (protogamePluginMode)
                {
                    case "info":
                        DataProxy._AppDomainData.Test = "Hello world!";
                        DataProxy._AppDomainData.ProvidesRunner = false;
                        foreach (Type t in target.GetTypes())
                        {
                            if (t.GetInterface("IOgmoRunner") != null)
                                DataProxy._AppDomainData.ProvidesRunner = true;
                        }
                        DataProxy._AppDomainData.Loaded = true;
                        break;
                    case "run":
                        // run the game
                        foreach (Type t in target.GetTypes())
                        {
                            if (t.GetInterface("IOgmoRunner") != null)
                            {
                                // start.
                                Thread thread = new Thread(() =>
                                    {
                                        object o = t.GetConstructor(Type.EmptyTypes).Invoke(null);
                                        try
                                        {
                                            object result = t.GetMethod("Start", BindingFlags.Public | BindingFlags.Instance).
                                                Invoke(o, new object[] { protogamePluginLevel });
                                            if (result == null)
                                            {
                                            }
                                            else if (result.GetType() == typeof(bool))
                                            {
                                                if ((bool)result != true)
                                                {
                                                    MessageBox.Show("An unexpected error occurred while running the game.", "Protogame Plugin");
                                                    return;
                                                }
                                            }
                                            else if (result.GetType() == typeof(string))
                                            {
                                                MessageBox.Show((string)result, "Protogame Plugin");
                                            }
                                        }
                                        catch (ThreadAbortException)
                                        {
                                            // Probably occurred due to a forceful termination of the game.
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show("An exception occurred while running the game:\r\n\r\n" + e.InnerException.Message + "\r\n\r\n" + e.InnerException.StackTrace, "Protogame Plugin");
                                        }

                                        DataProxy._AppDomainData.Loaded = true;
                                    });
                                thread.IsBackground = true;
                                thread.Start();
                                return;
                            }
                        }
                        break;
                }
            }
            finally
            {
                Environment.CurrentDirectory = olddir;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.m_AppDomain != null)
                AppDomain.Unload(this.m_AppDomain);
        }

        #endregion
    }
}
