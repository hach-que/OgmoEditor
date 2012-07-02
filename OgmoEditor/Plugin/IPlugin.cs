using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.Plugin
{
    public interface IPlugin
    {
        void Start();
        void Shutdown();

        void AttachMenus(MenuStrip menu);
        void NewProject(Project project);
        void LoadProject(Project project);
        void UnloadProject();
        void EditProject(Project project, AddTabCallback addTab);
        void SwitchLevel(LevelData.Level level);
    }

    public delegate void AddTabCallback(TabPage tab);
}
