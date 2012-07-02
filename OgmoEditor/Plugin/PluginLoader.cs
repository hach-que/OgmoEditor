using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace OgmoEditor.Plugin
{
    public class PluginLoader
    {
        private List<IPlugin> m_Plugins = new List<IPlugin>();

        public PluginLoader(string path)
        {
            // Weird behaviour regarding loading assemblies into AppDomains means
            // that all of the game-loading functionality of the Protogame plugin
            // needs to be loaded as a compile-time reference to OgmoEditor, rather
            // than being loaded as a runtime plugin.  This line causes OgmoEditor
            // to have a compile-time reference to the required functionality.
            OgmoEditor.Protogame.StaticAppDomainHelper.Dummy = true;

            // Load plugins.
            DirectoryInfo di = null;
            try
            {
                di = new DirectoryInfo(path);
            }
            catch (ArgumentException)
            {
                di = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, path));
            }

            foreach (FileInfo fi in di.GetFiles("OgmoPlugin.*.dll"))
            {
                Assembly a = Assembly.LoadFrom(fi.FullName);
                foreach (Type t in a.GetTypes())
                {
                    if (typeof(IPlugin).IsAssignableFrom(t))
                    {
                        IPlugin plugin = t.GetConstructor(Type.EmptyTypes).Invoke(null) as IPlugin;
                        this.m_Plugins.Add(plugin);
                    }
                }
            }
        }

        public void FireStart()
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.Start();
        }

        public void FireShutdown()
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.Shutdown();
        }

        public void FireAttachMenus(MenuStrip menu)
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.AttachMenus(menu);
        }

        public void FireNewProject(Project project)
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.NewProject(project);
        }

        public void FireLoadProject(Project project)
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.LoadProject(project);
        }

        public void FireUnloadProject()
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.UnloadProject();
        }

        public void FireSwitchLevel(LevelData.Level level)
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.SwitchLevel(level);
        }

        public void FireEditProject(ProjectEditors.ProjectEditor editor, Project project)
        {
            foreach (IPlugin plugin in this.m_Plugins)
                plugin.EditProject(project, editor.AddTab);
        }
    }
}
