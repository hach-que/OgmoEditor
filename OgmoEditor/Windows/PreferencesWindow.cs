using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OgmoEditor.Windows
{
    public partial class PreferencesWindow : Form
    {
        public PreferencesWindow()
        {
            InitializeComponent();
        }

        private void PreferencesWindow_Shown(object sender, EventArgs e)
        {
            maximizeCheckBox.Checked = Config.ConfigFile.StartMaximized;
            undoLimitTextBox.Text = Config.ConfigFile.UndoLimit.ToString();
            levelLimitTextBox.Text = Config.ConfigFile.LevelLimit.ToString();
            this.c_PluginPathTextBox.Text = Config.ConfigFile.PluginPath;

            clearHistoryButton.Enabled = Config.ConfigFile.RecentProjects.Count > 0;
        }

        private void PreferencesWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool restartRequired = (Config.ConfigFile.PluginPath != this.c_PluginPathTextBox.Text);
            Config.ConfigFile.StartMaximized = maximizeCheckBox.Checked;
            Config.ConfigFile.PluginPath = this.c_PluginPathTextBox.Text;
            OgmoParse.Parse(ref Config.ConfigFile.UndoLimit, undoLimitTextBox);
            OgmoParse.Parse(ref Config.ConfigFile.LevelLimit, levelLimitTextBox);
            if (restartRequired)
                MessageBox.Show("You must restart Ogmo Editor for the changes to take effect.");

            Config.Save();
            Ogmo.MainWindow.EnableEditing();
        }

        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            Config.ConfigFile.ClearRecentProjects();
            clearHistoryButton.Enabled = false;
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void c_BrowseButton_Click(object sender, EventArgs e)
        {
            if (this.c_BrowseFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.c_PluginPathTextBox.Text = this.c_BrowseFolder.SelectedPath;
            }
        }
    }
}
