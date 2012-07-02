using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Plugin;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using OgmoEditor;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using OgmoEditor.LevelData;
using OgmoEditor.Protogame;

namespace OgmoPlugin.Protogame
{
    public class ProtogamePlugin : MarshalByRefObject, IPlugin
    {
        private Project m_Project = null;
        private Level m_Level = null;
        private ToolStripMenuItem m_GameMenuItem = null;
        private ToolStripMenuItem m_GameStartMenuItem = null;
        private ToolStripMenuItem m_GameTerminateMenuItem = null;
        private GameDomain m_ActiveGame = null;
        private System.Windows.Forms.Timer m_ActiveGameTimer = null;
        private DebugWindow m_DebugWindow = null;

        #region IPlugin Members

        public ProtogamePlugin()
        {
        }

        public void Start()
        {
            this.m_ActiveGameTimer = new System.Windows.Forms.Timer();
            this.m_ActiveGameTimer.Tick += new EventHandler(this.TimerCallback);
            this.m_ActiveGameTimer.Interval = 500;
            this.m_ActiveGameTimer.Enabled = true;

            this.m_DebugWindow = new DebugWindow();
            this.m_DebugWindow.Show(Ogmo.MainWindow);
            this.m_DebugWindow.EditorVisible = false;
        }

        public void Shutdown()
        {
        }

        public void AttachMenus(MenuStrip menu)
        {
            this.m_GameMenuItem = new ToolStripMenuItem("Game") { Enabled = false };
            this.m_GameStartMenuItem = new ToolStripMenuItem("Start");
            this.m_GameStartMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            this.m_GameStartMenuItem.Click += new EventHandler(RunGame);
            this.m_GameTerminateMenuItem = new ToolStripMenuItem("Terminate");
            this.m_GameTerminateMenuItem.ShortcutKeys = Keys.Control | Keys.T;
            this.m_GameTerminateMenuItem.Click += new EventHandler(TerminateGame);
            this.m_GameTerminateMenuItem.Enabled = false;
            this.m_GameMenuItem.DropDownItems.Add(this.m_GameStartMenuItem);
            this.m_GameMenuItem.DropDownItems.Add(this.m_GameTerminateMenuItem);
            menu.Items.Insert(2, this.m_GameMenuItem);
        }

        private void RunGame(object sender, EventArgs e)
        {
            this.RecheckMenuEligibility();
            if (!this.m_GameMenuItem.Enabled)
                return;

            // Get data proxy.
            if (!this.m_Level.Save())
                return;
            this.m_ActiveGame = this.LoadGame(this.m_Project.CustomPluginEntries["ProtogamePath"], "run", this.m_Level.SavePath);
            OgmoEditor.Ogmo.MainWindow.StatusText = "The game is now running.";
        }

        private void TerminateGame(object sender, EventArgs e)
        {
            this.RecheckMenuEligibility();
            if (!this.m_GameTerminateMenuItem.Enabled || this.m_ActiveGame == null)
                return;

            this.m_ActiveGame.Dispose();
            this.m_ActiveGame = null;
            OgmoEditor.Ogmo.MainWindow.StatusText = "The game has been terminated forcefully.";
        }

        public void NewProject(Project project)
        {
        }

        public void LoadProject(Project project)
        {
            this.m_Project = project;
            this.RecheckMenuEligibility();
        }

        public void UnloadProject()
        {
            this.m_Project = null;
            this.m_Level = null;
            this.RecheckMenuEligibility();
        }

        public void SwitchLevel(Level level)
        {
            this.m_Level = level;
        }

        public void EditProject(Project project, AddTabCallback addTab)
        {
            TabPage tab = new TabPage("Protogame");
            tab.Controls.Add(new ProjectEditorTab(this, project) { Dock = DockStyle.Fill });
            addTab(tab);
        }

        #region AppDomain and Game Loading

        internal GameDomain LoadGame(string path, string mode, string level)
        {
            return new GameDomain(path, mode, level);
        }

        private void TimerCallback(object sender, EventArgs e)
        {
            if (this.m_ActiveGame != null)
            {
                IDataProxy proxy = this.m_ActiveGame.Proxy;

                // Check if the game has exited.
                if (proxy.Loaded)
                {
                    // game is finished.
                    OgmoEditor.Ogmo.MainWindow.StatusText = "The game has exited.";
                    this.m_ActiveGame.Dispose();
                    this.m_ActiveGame = null;
                    this.m_DebugWindow.EditorVisible = false;
                    this.RecheckMenuEligibility();
                    return;
                }

                // Update the focused object for the debugging window if needed.
                this.m_DebugWindow.FocusedObject = proxy.FocusedObject;
            }

            this.m_DebugWindow.EditorVisible = Ogmo.LayersWindow.EditorVisible && this.m_ActiveGame != null;
            this.RecheckMenuEligibility();
        }

        private void RecheckMenuEligibility()
        {
            this.m_GameStartMenuItem.Text = "Start";

            if (this.m_ActiveGame != null)
            {
                this.m_GameMenuItem.Enabled = true;
                this.m_GameStartMenuItem.Text = "Running...";
                this.m_GameStartMenuItem.Enabled = false;
                this.m_GameTerminateMenuItem.Enabled = true;
                return;
            }

            if (this.m_Project == null || !this.m_Project.CustomPluginEntries.Keys.Contains("ProtogamePath") ||
                !File.Exists(this.m_Project.CustomPluginEntries["ProtogamePath"]))
            {
                this.m_GameMenuItem.Enabled = false;
                this.m_GameStartMenuItem.Enabled = false;
                this.m_GameTerminateMenuItem.Enabled = false;
                return;
            }

            // Get data proxy.
            using (GameDomain game = this.LoadGame(this.m_Project.CustomPluginEntries["ProtogamePath"], "info", null))
            {
                IDataProxy proxy = game.Proxy;
                int attempts = 0;
                while (!proxy.Loaded && (attempts++) < 10) Thread.Sleep(100);
                if (attempts == 10)
                {
                    MessageBox.Show("Game assembly did not load in time.", "Protogame Plugin");
                    return;
                }

                // Get the test message.
                this.m_GameMenuItem.Enabled = proxy.ProvidesRunner;
                this.m_GameStartMenuItem.Enabled = proxy.ProvidesRunner;
                this.m_GameTerminateMenuItem.Enabled = false;
            }
        }

        #endregion

        #endregion
    }
}
