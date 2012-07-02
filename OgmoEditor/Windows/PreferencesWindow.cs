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
            maximizeCheckBox.Checked = Properties.Settings.Default.StartMaximized;
            undoLimitTextBox.Text = Properties.Settings.Default.UndoLimit.ToString();
            levelLimitTextBox.Text = Properties.Settings.Default.LevelLimit.ToString();
            this.c_PluginPathTextBox.Text = Properties.Settings.Default.PluginPath;

            clearHistoryButton.Enabled = Properties.Settings.Default.RecentProjects.Count > 0;
        }

        private void PreferencesWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool restartRequired = (Properties.Settings.Default.PluginPath != this.c_PluginPathTextBox.Text);
            Properties.Settings.Default.StartMaximized = maximizeCheckBox.Checked;
            Properties.Settings.Default.PluginPath = this.c_PluginPathTextBox.Text;
            if (restartRequired)
                MessageBox.Show("You must restart Ogmo Editor for the changes to take effect.", "Restart Required");

            try
            {
                Properties.Settings.Default.UndoLimit = Convert.ToInt32(undoLimitTextBox.Text);
            }
            catch
            { }

            try
            {
                Properties.Settings.Default.LevelLimit = Convert.ToInt32(levelLimitTextBox.Text);
            }
            catch
            { }

            Properties.Settings.Default.Save();
            Ogmo.MainWindow.EnableEditing();
        }

        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            Ogmo.ClearRecentProjects();
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
