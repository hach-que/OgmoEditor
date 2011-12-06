﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridSelectionTool : GridTool
    {
        private bool drawing;
        private Point drawStart;
        private Point drawTo;

        public GridSelectionTool()
            : base("Select", "selection.png")
        {

        }

        public override void Draw(Content content)
        {
            if (drawing)
            {
                Rectangle draw = LayerEditor.Layer.GetGridRectangle(drawStart, drawTo);
                if (LevelEditor.Level.Bounds.IntersectsWith(draw))
                    content.DrawFillRect(draw, Microsoft.Xna.Framework.Color.Yellow * .5f);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing)
            {
                drawing = false;
                drawTo = LayerEditor.MouseSnapPosition;

                Rectangle rect = LayerEditor.Layer.GetGridRectangle(drawStart, drawTo);
                rect.X /= LayerEditor.Layer.Definition.Grid.Width;
                rect.Width /= LayerEditor.Layer.Definition.Grid.Width;
                rect.Y /= LayerEditor.Layer.Definition.Grid.Height;
                rect.Height /= LayerEditor.Layer.Definition.Grid.Height;
                LevelEditor.Perform(new GridSelectAction(LayerEditor.Layer, rect));
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (drawing)
                drawTo = LayerEditor.MouseSnapPosition;
        }
    }
}
