﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.LevelEditors.Tools.GridTools
{
    public class GridRectangleTool : GridTool
    {
        private bool drawing;
        private bool drawMode;
        private Point drawStart;
        private Point drawTo;

        public GridRectangleTool()
            : base("Rectangle", "rectangle.png")
        {
            drawing = false;
        }

        public override void Draw()
        {
            if (drawing)
            {
                Rectangle draw = LayerEditor.Layer.GetGridRectangle(drawStart, drawTo);
                if (LevelEditor.Level.Bounds.IntersectsWith(draw))
                    Ogmo.EditorDraw.DrawFillRect(draw, (drawMode ? LayerEditor.Layer.Definition.Color.ToXNA() : LayerEditor.Layer.Definition.Color.Invert().ToXNA()) * .5f);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
            drawMode = true;
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (drawing && drawMode)
                drawRect();
        }

        public override void OnMouseRightDown(Point location)
        {
            drawTo = drawStart = LayerEditor.MouseSnapPosition;
            drawing = true;
            drawMode = false;
        }

        public override void OnMouseRightUp(Point location)
        {
            if (drawing && !drawMode)
                drawRect();
        }

        public override void OnMouseMove(Point location)
        {
            if (drawing)
                drawTo = LayerEditor.MouseSnapPosition;
        }
        
        /*
         *  Helpers
         */
        private void drawRect()
        {
            drawing = false;
            Rectangle draw = LayerEditor.Layer.GetGridRectangle(drawStart, drawTo);
            if (LevelEditor.Level.Bounds.IntersectsWith(draw))
            {
                draw = LayerEditor.Layer.Definition.ConvertToGrid(draw);
                LevelEditor.Perform(new GridRectangleAction(LayerEditor.Layer, draw, drawMode));
            }
        }
    }
}
