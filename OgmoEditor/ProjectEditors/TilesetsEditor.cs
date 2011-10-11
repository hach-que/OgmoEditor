﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;
using System.IO;
using System.Diagnostics;

namespace OgmoEditor.ProjectEditors
{
    public partial class TilesetsEditor : UserControl, IProjectChanger
    {
        private const string DEFAULT_NAME = "NewTileset";

        private List<Tileset> tilesets;
        private string directory;

        public TilesetsEditor()
        {
            InitializeComponent();
        }

        public string ErrorCheck()
        {
            return "";
        }

        public void LoadFromProject(Project project)
        {
            tilesets = project.Tilesets;
            foreach (var t in tilesets)
                listBox.Items.Add(t.Name);

            directory = project.SavedDirectory;
        }

        public void ApplyToProject(Project project)
        {

        }

        private void setControlsFromTileset(Tileset t)
        {
            removeButton.Enabled = true;
            moveUpButton.Enabled = listBox.SelectedIndex > 0;
            moveDownButton.Enabled = listBox.SelectedIndex < listBox.Items.Count - 1;

            previewBox.Enabled = true;
            nameTextBox.Enabled = true;
            imageFileTextBox.Enabled = true;
            imageFileButton.Enabled = true;
            imageFileWarningLabel.Enabled = true;
            tileSizeXTextBox.Enabled = true;
            tileSizeYTextBox.Enabled = true;
            tileSpacingTextBox.Enabled = true;

            nameTextBox.Text = t.Name;
            imageFileTextBox.Text = t.Path;
            tileSizeXTextBox.Text = t.TileSize.Width.ToString();
            tileSizeYTextBox.Text = t.TileSize.Height.ToString();
            tileSpacingTextBox.Text = t.TileSep.ToString();

            imageFileWarningLabel.Visible = !checkImageFile();
            loadPreview();
        }

        private void disableControls()
        {
            removeButton.Enabled = false;
            moveUpButton.Enabled = false;
            moveDownButton.Enabled = false;

            previewBox.Enabled = false;
            nameTextBox.Enabled = false;
            imageFileTextBox.Enabled = false;
            imageFileButton.Enabled = false;
            imageFileWarningLabel.Enabled = false;
            tileSizeXTextBox.Enabled = false;
            tileSizeYTextBox.Enabled = false;
            tileSpacingTextBox.Enabled = false;

            imageFileWarningLabel.Visible = false;
            clearPreview();
        }

        private string getNewName()
        {
            int i = 0;
            string name;

            do
            {
                name = DEFAULT_NAME + i.ToString();
                i++;
            }
            while (tilesets.Find(t => t.Name == name) != null);

            return name;
        }

        private bool checkImageFile()
        {
            return File.Exists(Util.GetPathAbsolute(imageFileTextBox.Text, directory));
        }

        private void loadPreview()
        {
            previewBox.ImageLocation = Util.GetPathAbsolute(imageFileTextBox.Text, directory);
            if (previewBox.Image == null)
            {
                previewBox.Padding = new Padding(0, 0, 0, 0);
                imageSizeLabel.Visible = false;
                totalTilesLabel.Visible = false;
            }
            else
            {
                previewBox.Padding = new Padding(
                    previewBox.Width / 2 - previewBox.Image.Width / 2,
                    previewBox.Height / 2 - previewBox.Image.Height / 2, 0, 0);
                imageSizeLabel.Visible = true;
                imageSizeLabel.Text = "Image Size: " + previewBox.Image.Width + " x " + previewBox.Image.Height;
                totalTilesLabel.Visible = true;
                updateTotalTiles();
            }
        }

        private void clearPreview()
        {
            previewBox.Image = null;
            imageSizeLabel.Visible = false;
            totalTilesLabel.Visible = false;
        }

        private void updateTotalTiles()
        {
            int tw = tilesets[listBox.SelectedIndex].TileSize.Width;
            int th = tilesets[listBox.SelectedIndex].TileSize.Height;
            int sep = tilesets[listBox.SelectedIndex].TileSep;

            int across = 0;
            for (int i = 0; i + tw <= previewBox.Image.Width; i += tw + sep)
                across++;

            int down = 0;
            for (int i = 0; i + th <= previewBox.Image.Height; i += th + sep)
                down++;

            int total = across * down;

            totalTilesLabel.Text = "Tiles: " + across.ToString() + " x " + down.ToString() + " (" + (across * down).ToString() + " total)";
        }

        /*
         *  Events
         */
        private void addButton_Click(object sender, EventArgs e)
        {
            Tileset t = new Tileset();
            t.Name = getNewName();
            tilesets.Add(t);
            listBox.SelectedIndex = listBox.Items.Add(t.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            tilesets.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            Tileset temp = tilesets[index];
            tilesets[index] = tilesets[index - 1];
            tilesets[index - 1] = temp;

            listBox.Items[index] = tilesets[index].Name;
            listBox.Items[index - 1] = tilesets[index - 1].Name;
            listBox.SelectedIndex = index - 1;
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;

            Tileset temp = tilesets[index];
            tilesets[index] = tilesets[index + 1];
            tilesets[index + 1] = temp;

            listBox.Items[index] = tilesets[index].Name;
            listBox.Items[index + 1] = tilesets[index + 1].Name;
            listBox.SelectedIndex = index + 1;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                disableControls();
            else
                setControlsFromTileset(tilesets[listBox.SelectedIndex]);
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            tilesets[listBox.SelectedIndex].Name = nameTextBox.Text;
            listBox.Items[listBox.SelectedIndex] = nameTextBox.Text;
        }

        private void tileSizeXTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSize, tileSizeXTextBox, tileSizeYTextBox);
            updateTotalTiles();
        }

        private void tileSizeYTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSize, tileSizeXTextBox, tileSizeYTextBox);
            updateTotalTiles();
        }

        private void tileSpacingTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSep, tileSpacingTextBox);
            updateTotalTiles();
        }

        private void imageFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Tileset.FILE_FILTER;
            dialog.CheckFileExists = true;

            if (checkImageFile())
                dialog.InitialDirectory = Path.GetFullPath(Util.GetPathAbsolute(imageFileTextBox.Text, directory));
            else
                dialog.InitialDirectory = directory;

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            imageFileTextBox.Text = Util.GetFilePathRelativeTo(dialog.FileName, directory);
            imageFileWarningLabel.Visible = !checkImageFile();
            loadPreview();

            tilesets[listBox.SelectedIndex].Path = imageFileTextBox.Text;
        }
    }
}
