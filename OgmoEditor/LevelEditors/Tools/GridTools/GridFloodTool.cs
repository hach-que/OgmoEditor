﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridFloodTool : GridTool
    {
        public GridFloodTool()
            : base("Grid Flood", "flood.png", System.Windows.Forms.Keys.F)
        {
            
        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            LevelEditor.Perform(new GridFloodAction(LayerEditor.Layer, location.X, location.Y, true)); 
        }

        public override void OnMouseRightClick(System.Drawing.Point location)
        {
            if (!LevelEditor.Level.Bounds.Contains(location))
                return;

            location = LayerEditor.Layer.Definition.ConvertToGrid(location);
            LevelEditor.Perform(new GridFloodAction(LayerEditor.Layer, location.X, location.Y, false));
        }
    }
}