using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using OgmoEditor.Protogame;

namespace OgmoPlugin.Protogame
{
    public partial class ProjectEditorTab : UserControl
    {
        private OgmoEditor.Project m_Project;
        private ProtogamePlugin m_Plugin;

        public ProjectEditorTab(ProtogamePlugin plugin, OgmoEditor.Project project)
        {
            InitializeComponent();

            this.m_Plugin = plugin;
            this.m_Project = project;

            if (this.m_Project.CustomPluginEntries.Keys.Contains("ProtogamePath"))
            {
                this.c_ProtogamePath.Text = this.m_Project.CustomPluginEntries["ProtogamePath"] as string;
                this.RefreshAssemblyInformation();
            }
        }

        private void c_ProtogameBrowse_Click(object sender, EventArgs e)
        {
            if (this.c_ProtogameOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.c_ProtogamePath.Text = this.c_ProtogameOpenFileDialog.FileName;
            }
        }

        private void RefreshAssemblyInformation()
        {
            if (!File.Exists(this.c_ProtogamePath.Text))
                return;

            // Get data proxy.
            using (GameDomain game = this.m_Plugin.LoadGame(this.c_ProtogamePath.Text, "info", null))
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
                if (proxy.ProvidesRunner)
                    this.c_AssemblyInformationLabel.Text = "This game implements IOgmoRunner and will be debuggable.";
                else
                    this.c_AssemblyInformationLabel.Text = "No class implemented IOgmoRunner.  Implement this interface in your game and then try again.";
            }
        }

        private void c_ProtogamePath_TextChanged(object sender, EventArgs e)
        {
            this.m_Project.CustomPluginEntries["ProtogamePath"] = this.c_ProtogamePath.Text;
        }

        private void c_ReloadAssembly_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.c_ProtogamePath.Text))
                this.RefreshAssemblyInformation();
            else
                MessageBox.Show("The path to the specified file does not exist.");
        }
    }
}
