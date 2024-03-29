﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Runtime.Serialization;
using System.Drawing;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Collections;
using OgmoEditor.Definitions.ValueDefinitions;
using OgmoEditor.Definitions;
using OgmoEditor.ProjectEditors;
using OgmoEditor.LevelEditors;
using SerializeDictionary;
using System.Deployment.Application;

namespace OgmoEditor
{
    [XmlRoot("project")]
    public class Project
    {
        public enum AngleExportMode { Radians, Degrees };

        //Serialized project properties
        public string OgmoVersion;
        public string Name;
        public OgmoColor BackgroundColor;
        public OgmoColor GridColor;
        public Size LevelDefaultSize;
        public Size LevelMinimumSize;
        public Size LevelMaximumSize;
        public string Filename;
        public AngleExportMode AngleMode;
        public bool CameraEnabled;
        public Size CameraSize;
        public bool ExportCameraPosition;
        public SerializableDictionary<string, string> CustomPluginEntries;

        //Definitions
        public List<ValueDefinition> LevelValueDefinitions;
        public List<LayerDefinition> LayerDefinitions;
        public List<Tileset> Tilesets;
        public List<EntityDefinition> EntityDefinitions;

        //Events
        public event Ogmo.ProjectCallback OnPathChanged;

        public Project()
        {
            //Default project properties
            Name = Ogmo.NEW_PROJECT_NAME;
            BackgroundColor = OgmoColor.DefaultBackgroundColor;
            GridColor = OgmoColor.DefaultGridColor;
            Filename = "";
            LevelDefaultSize = LevelMinimumSize = LevelMaximumSize = new Size(640, 480);
            CameraEnabled = false;
            CameraSize = new Size(640, 480);
            ExportCameraPosition = false;
            CustomPluginEntries = new SerializableDictionary<string, string>();

            //Definitions
            LevelValueDefinitions = new List<ValueDefinition>();
            LayerDefinitions = new List<LayerDefinition>();
            Tilesets = new List<Tileset>();
            EntityDefinitions = new List<EntityDefinition>();
        }

        public void InitDefault()
        {
            //The default layer
            GridLayerDefinition def = new GridLayerDefinition();
            def.Name = "NewLayer0";
            def.Grid = new Size(16, 16);
            LayerDefinitions.Add(def);
        }

        public void CloneFrom(Project copy)
        {
            //Default project properties
            OgmoVersion = copy.OgmoVersion;
            Name = copy.Name;
            BackgroundColor = copy.BackgroundColor;
            GridColor = copy.GridColor;
            Filename = copy.Filename;
            LevelDefaultSize = copy.LevelDefaultSize;
            LevelMinimumSize = copy.LevelMinimumSize;
            LevelMaximumSize = copy.LevelMaximumSize;
            AngleMode = copy.AngleMode;
            CameraEnabled = copy.CameraEnabled;
            CameraSize = copy.CameraSize;
            ExportCameraPosition = copy.ExportCameraPosition;
            CustomPluginEntries = copy.CustomPluginEntries;

            //Definitions
            CustomPluginEntries = new SerializableDictionary<string, string>();
            foreach (var d in copy.CustomPluginEntries)
                CustomPluginEntries.Add(d.Key, d.Value);

            LevelValueDefinitions = new List<ValueDefinition>();
            foreach (var d in copy.LevelValueDefinitions)
                LevelValueDefinitions.Add(d.Clone());

            LayerDefinitions = new List<LayerDefinition>();   
            foreach (var d in copy.LayerDefinitions)
                LayerDefinitions.Add(d.Clone());
  
            Tilesets = new List<Tileset>();
            foreach (var d in copy.Tilesets)
                Tilesets.Add(d.Clone());

            EntityDefinitions = new List<EntityDefinition>();
            foreach (var d in copy.EntityDefinitions)
                EntityDefinitions.Add(d.Clone());
        }

        public string ErrorCheck()
        {
            string s = "";

            /*
             *  PROJECT SETTINGS
             */

            s += OgmoParse.CheckNonblankString(Name, "Project Name");
            s += OgmoParse.CheckPosSize(LevelDefaultSize, "Default Level");
            s += OgmoParse.CheckPosSize(LevelMinimumSize, "Minimum Level");
            s += OgmoParse.CheckPosSize(LevelMaximumSize, "Maximum Level");
            s += OgmoParse.CheckDefinitionList(LevelValueDefinitions, "Level");

            /*
             *  LAYER DEFINITIONS
             */

            //Must have at least 1 layer
            if (LayerDefinitions.Count == 0)
                s += OgmoParse.Error("No layers are defined");

            //Check for duplicates and blanks
            s += OgmoParse.CheckDefinitionList(LayerDefinitions);

            foreach (var l in LayerDefinitions)
            {
                //All grid sizes must be > 0
                if (l.Grid.Width <= 0)
                    s += OgmoParse.Error("Layer \"" + l.Name + "\" has a grid cell width <= 0");
                if (l.Grid.Height <= 0)
                    s += OgmoParse.Error("Layer \"" + l.Name + "\" has a grid cell height <= 0");
            }

            //Must have a tileset if you have a tile layer
            if (LayerDefinitions.Find(l => l is TileLayerDefinition) != null && Tilesets.Count == 0)
                s += OgmoParse.Error("Tile layer(s) are defined, but no tilesets are defined");

            //Must have an entity if you have an entity layer
            if (LayerDefinitions.Find(l => l is EntityLayerDefinition) != null && EntityDefinitions.Count == 0)
                s += OgmoParse.Error("Object layer(s) are defined, but no objects are defined");

            /*
             *  TILESETS
             */

            //Check for duplicates and blanks
            s += OgmoParse.CheckDefinitionList(Tilesets);

            foreach (var t in Tilesets)
            {
                //File must exist
                s += OgmoParse.CheckPath(t.FilePath, SavedDirectory, "Tileset \"" + t.Name + "\" image file");
            }

            /*
             *  ENTITIES
             */

            //Check for duplicates and blanks
            s += OgmoParse.CheckDefinitionList(EntityDefinitions);

            foreach (var o in EntityDefinitions)
            {
                //Check Entity values for reserved words
                s += OgmoParse.CheckEntityValues(o, o.ValueDefinitions);

                //Image file must exist if it is using an image file to draw
                if (o.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
                    s += OgmoParse.CheckPath(o.ImageDefinition.ImagePath, SavedDirectory, "Object \"" + o.Name + "\" image file");

                //An entity can't have a tiled image and be rotatable
                if (o.ImageDefinition.Tiled && o.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && o.Rotatable)
                    s += OgmoParse.Error("Object \"" + o.Name + "\" has a tiled image and is rotatable. These features are incompatible in this version of Ogmo Editor");
            }

            /*
             *  VALUES
             */

            s += OgmoParse.CheckLevelValues(LevelValueDefinitions);

            return s;
        }

        [XmlIgnore]
        public string SavedDirectory
        {
            get
            {
                string dir = Filename;
                if (dir == "")
                    return "";

                string filename = Path.GetFileName(dir);
                return dir.Remove(dir.IndexOf(filename) - 1);
            }
        }

        public string GetPath(string path)
        {
            return SavedDirectory + Path.DirectorySeparatorChar + path;
        }

        public string ExportAngle(float angle)
        {
            if (AngleMode == AngleExportMode.Radians)
                return (angle * Util.DEGTORAD).ToString();
            else
                return angle.ToString();
        }

        public float ImportAngle(string angle)
        {
            if (AngleMode == AngleExportMode.Radians)
                return Convert.ToSingle(angle) * Util.RADTODEG;
            else
                return Convert.ToSingle(angle);
        }

        /*
         *  Saving the project file
         */
        public void Save()
        {
            //If it hasn't been saved yet, go to SaveAs
            if (Filename == "")
            {
                if (!SaveAs())
                    return;
            }

            writeTo(Filename);
        }

        public bool SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (Filename == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = SavedDirectory;
            dialog.RestoreDirectory = true;
            dialog.FileName = Name;
            dialog.DefaultExt = Ogmo.PROJECT_EXT;
            dialog.OverwritePrompt = true;
            dialog.Filter = Ogmo.PROJECT_FILTER;

            //Show dialog, handle cancel
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            Filename = dialog.FileName;
            if (OnPathChanged != null)
                OnPathChanged(this);

            return true;
        }

        private void writeTo(string filename)
        {
            //Set the current Ogmo Editor version in the project file
            if (ApplicationDeployment.IsNetworkDeployed)
                OgmoVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            else
                OgmoVersion = new Version(1, 0).ToString();

            XmlSerializer xs = new XmlSerializer(typeof(Project));
            Stream stream = new FileStream(filename, FileMode.Create);
            xs.Serialize(stream, this);
            stream.Close();
        }
    }
}
