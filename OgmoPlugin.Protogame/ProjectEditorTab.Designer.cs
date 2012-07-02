namespace OgmoPlugin.Protogame
{
    partial class ProjectEditorTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_EditorLabel = new System.Windows.Forms.Label();
            this.c_ProtogameBrowse = new System.Windows.Forms.Button();
            this.c_ProtogamePath = new System.Windows.Forms.TextBox();
            this.c_ProtogameOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.c_NextStepLabel = new System.Windows.Forms.Label();
            this.c_ReloadAssembly = new System.Windows.Forms.Button();
            this.c_ProtogameWebsiteLink = new System.Windows.Forms.LinkLabel();
            this.c_LoadedInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.c_AssemblyInformationLabel = new System.Windows.Forms.Label();
            this.c_LoadedInformationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_EditorLabel
            // 
            this.c_EditorLabel.Location = new System.Drawing.Point(14, 16);
            this.c_EditorLabel.Name = "c_EditorLabel";
            this.c_EditorLabel.Size = new System.Drawing.Size(334, 52);
            this.c_EditorLabel.TabIndex = 0;
            this.c_EditorLabel.Text = "To test the level using the Protogame-based game you must set the path to the gam" +
    "e executable.  The level will be automatically saved and executed in the game.";
            // 
            // c_ProtogameBrowse
            // 
            this.c_ProtogameBrowse.Location = new System.Drawing.Point(278, 79);
            this.c_ProtogameBrowse.Name = "c_ProtogameBrowse";
            this.c_ProtogameBrowse.Size = new System.Drawing.Size(70, 25);
            this.c_ProtogameBrowse.TabIndex = 1;
            this.c_ProtogameBrowse.Text = "Browse";
            this.c_ProtogameBrowse.UseVisualStyleBackColor = true;
            this.c_ProtogameBrowse.Click += new System.EventHandler(this.c_ProtogameBrowse_Click);
            // 
            // c_ProtogamePath
            // 
            this.c_ProtogamePath.Location = new System.Drawing.Point(17, 82);
            this.c_ProtogamePath.Name = "c_ProtogamePath";
            this.c_ProtogamePath.Size = new System.Drawing.Size(245, 20);
            this.c_ProtogamePath.TabIndex = 2;
            this.c_ProtogamePath.TextChanged += new System.EventHandler(this.c_ProtogamePath_TextChanged);
            // 
            // c_ProtogameOpenFileDialog
            // 
            this.c_ProtogameOpenFileDialog.FileName = "game.exe";
            this.c_ProtogameOpenFileDialog.Filter = "Executable files|*.exe";
            // 
            // c_NextStepLabel
            // 
            this.c_NextStepLabel.Location = new System.Drawing.Point(14, 130);
            this.c_NextStepLabel.Name = "c_NextStepLabel";
            this.c_NextStepLabel.Size = new System.Drawing.Size(334, 52);
            this.c_NextStepLabel.TabIndex = 3;
            this.c_NextStepLabel.Text = "Once you have selected the path to the executable, you must then reload the game " +
    "assembly information using the button below.  This will update the required hook" +
    "s to start the game.";
            // 
            // c_ReloadAssembly
            // 
            this.c_ReloadAssembly.Location = new System.Drawing.Point(17, 185);
            this.c_ReloadAssembly.Name = "c_ReloadAssembly";
            this.c_ReloadAssembly.Size = new System.Drawing.Size(139, 25);
            this.c_ReloadAssembly.TabIndex = 4;
            this.c_ReloadAssembly.Text = "Reload Assembly";
            this.c_ReloadAssembly.UseVisualStyleBackColor = true;
            this.c_ReloadAssembly.Click += new System.EventHandler(this.c_ReloadAssembly_Click);
            // 
            // c_ProtogameWebsiteLink
            // 
            this.c_ProtogameWebsiteLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_ProtogameWebsiteLink.AutoSize = true;
            this.c_ProtogameWebsiteLink.Location = new System.Drawing.Point(279, 453);
            this.c_ProtogameWebsiteLink.Name = "c_ProtogameWebsiteLink";
            this.c_ProtogameWebsiteLink.Size = new System.Drawing.Size(75, 13);
            this.c_ProtogameWebsiteLink.TabIndex = 5;
            this.c_ProtogameWebsiteLink.TabStop = true;
            this.c_ProtogameWebsiteLink.Text = "protogame.org";
            // 
            // c_LoadedInformationGroupBox
            // 
            this.c_LoadedInformationGroupBox.Controls.Add(this.c_AssemblyInformationLabel);
            this.c_LoadedInformationGroupBox.Location = new System.Drawing.Point(17, 229);
            this.c_LoadedInformationGroupBox.Name = "c_LoadedInformationGroupBox";
            this.c_LoadedInformationGroupBox.Size = new System.Drawing.Size(331, 130);
            this.c_LoadedInformationGroupBox.TabIndex = 6;
            this.c_LoadedInformationGroupBox.TabStop = false;
            this.c_LoadedInformationGroupBox.Text = "Loaded Assembly Information";
            // 
            // c_AssemblyInformationLabel
            // 
            this.c_AssemblyInformationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_AssemblyInformationLabel.Location = new System.Drawing.Point(3, 16);
            this.c_AssemblyInformationLabel.Name = "c_AssemblyInformationLabel";
            this.c_AssemblyInformationLabel.Padding = new System.Windows.Forms.Padding(10);
            this.c_AssemblyInformationLabel.Size = new System.Drawing.Size(325, 111);
            this.c_AssemblyInformationLabel.TabIndex = 0;
            this.c_AssemblyInformationLabel.Text = "No assembly is currently loaded.";
            // 
            // ProjectEditorTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c_LoadedInformationGroupBox);
            this.Controls.Add(this.c_ProtogameWebsiteLink);
            this.Controls.Add(this.c_ReloadAssembly);
            this.Controls.Add(this.c_NextStepLabel);
            this.Controls.Add(this.c_ProtogamePath);
            this.Controls.Add(this.c_ProtogameBrowse);
            this.Controls.Add(this.c_EditorLabel);
            this.Name = "ProjectEditorTab";
            this.Size = new System.Drawing.Size(366, 480);
            this.c_LoadedInformationGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label c_EditorLabel;
        private System.Windows.Forms.Button c_ProtogameBrowse;
        private System.Windows.Forms.TextBox c_ProtogamePath;
        private System.Windows.Forms.OpenFileDialog c_ProtogameOpenFileDialog;
        private System.Windows.Forms.Label c_NextStepLabel;
        private System.Windows.Forms.Button c_ReloadAssembly;
        private System.Windows.Forms.LinkLabel c_ProtogameWebsiteLink;
        private System.Windows.Forms.GroupBox c_LoadedInformationGroupBox;
        private System.Windows.Forms.Label c_AssemblyInformationLabel;
    }
}
