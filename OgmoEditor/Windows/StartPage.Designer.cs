﻿namespace OgmoEditor.Windows
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.recentLabel = new System.Windows.Forms.Label();
            this.recentPanel = new System.Windows.Forms.Panel();
            this.twitterPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.recentPanel.SuspendLayout();
            this.twitterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(637, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // recentLabel
            // 
            this.recentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recentLabel.Location = new System.Drawing.Point(3, 5);
            this.recentLabel.Name = "recentLabel";
            this.recentLabel.Size = new System.Drawing.Size(152, 17);
            this.recentLabel.TabIndex = 1;
            this.recentLabel.Text = "Recent Projects";
            this.recentLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // recentPanel
            // 
            this.recentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.recentPanel.BackColor = System.Drawing.Color.White;
            this.recentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recentPanel.Controls.Add(this.recentLabel);
            this.recentPanel.Location = new System.Drawing.Point(3, 212);
            this.recentPanel.Name = "recentPanel";
            this.recentPanel.Size = new System.Drawing.Size(180, 265);
            this.recentPanel.TabIndex = 2;
            // 
            // twitterPanel
            // 
            this.twitterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.twitterPanel.BackColor = System.Drawing.Color.White;
            this.twitterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.twitterPanel.Controls.Add(this.label1);
            this.twitterPanel.Location = new System.Drawing.Point(460, 212);
            this.twitterPanel.Name = "twitterPanel";
            this.twitterPanel.Size = new System.Drawing.Size(180, 265);
            this.twitterPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "@OgmoEditor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.twitterPanel);
            this.Controls.Add(this.recentPanel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartPage";
            this.Size = new System.Drawing.Size(640, 480);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.recentPanel.ResumeLayout(false);
            this.twitterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label recentLabel;
        private System.Windows.Forms.Panel recentPanel;
        private System.Windows.Forms.Panel twitterPanel;
        private System.Windows.Forms.Label label1;
    }
}
