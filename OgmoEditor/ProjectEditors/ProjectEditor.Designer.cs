﻿namespace OgmoEditor.ProjectEditors
{
    partial class ProjectEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.objectsTabPage = new System.Windows.Forms.TabPage();
            this.tilesetsTabPage = new System.Windows.Forms.TabPage();
            this.layersTabPage = new System.Windows.Forms.TabPage();
            this.layersEditor = new OgmoEditor.ProjectEditors.LayersEditor();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.settingsEditor = new OgmoEditor.ProjectEditors.SettingsEditor();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.layersTabPage.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(504, 521);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 38);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(344, 521);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(154, 38);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // objectsTabPage
            // 
            this.objectsTabPage.Location = new System.Drawing.Point(4, 22);
            this.objectsTabPage.Name = "objectsTabPage";
            this.objectsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.objectsTabPage.Size = new System.Drawing.Size(573, 490);
            this.objectsTabPage.TabIndex = 3;
            this.objectsTabPage.Text = "Objects";
            this.objectsTabPage.UseVisualStyleBackColor = true;
            // 
            // tilesetsTabPage
            // 
            this.tilesetsTabPage.Location = new System.Drawing.Point(4, 22);
            this.tilesetsTabPage.Name = "tilesetsTabPage";
            this.tilesetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tilesetsTabPage.Size = new System.Drawing.Size(573, 490);
            this.tilesetsTabPage.TabIndex = 2;
            this.tilesetsTabPage.Text = "Tilesets";
            this.tilesetsTabPage.UseVisualStyleBackColor = true;
            // 
            // layersTabPage
            // 
            this.layersTabPage.Controls.Add(this.layersEditor);
            this.layersTabPage.Location = new System.Drawing.Point(4, 22);
            this.layersTabPage.Name = "layersTabPage";
            this.layersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.layersTabPage.Size = new System.Drawing.Size(573, 490);
            this.layersTabPage.TabIndex = 1;
            this.layersTabPage.Text = "Layers";
            this.layersTabPage.UseVisualStyleBackColor = true;
            // 
            // layersEditor
            // 
            this.layersEditor.Location = new System.Drawing.Point(0, 0);
            this.layersEditor.Name = "layersEditor";
            this.layersEditor.Size = new System.Drawing.Size(573, 490);
            this.layersEditor.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Controls.Add(this.settingsEditor);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(573, 490);
            this.settingsTabPage.TabIndex = 0;
            this.settingsTabPage.Text = "Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // settingsEditor
            // 
            this.settingsEditor.Location = new System.Drawing.Point(0, 0);
            this.settingsEditor.Name = "settingsEditor";
            this.settingsEditor.Size = new System.Drawing.Size(573, 490);
            this.settingsEditor.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.layersTabPage);
            this.tabControl.Controls.Add(this.tilesetsTabPage);
            this.tabControl.Controls.Add(this.objectsTabPage);
            this.tabControl.Location = new System.Drawing.Point(2, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(581, 516);
            this.tabControl.TabIndex = 1;
            // 
            // ProjectEditor
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "ProjectEditor";
            this.Text = "ProjectEditor";
            this.layersTabPage.ResumeLayout(false);
            this.settingsTabPage.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.TabPage objectsTabPage;
        private System.Windows.Forms.TabPage tilesetsTabPage;
        private System.Windows.Forms.TabPage layersTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabControl tabControl;
        private SettingsEditor settingsEditor;
        private LayersEditor layersEditor;
    }
}