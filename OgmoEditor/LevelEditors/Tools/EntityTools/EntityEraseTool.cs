﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityEraseTool : EntityTool
    {
        private Point mouseStart;
        private bool drawing;

        public EntityEraseTool()
            : base("Entity Erase", "eraser.png", System.Windows.Forms.Keys.E)
        {
            drawing = false;
        }

        public override void Draw(Content content)
        {
            if (drawing)
            {
                int x = Math.Min(mouseStart.X, LevelEditor.MousePosition.X);
                int y = Math.Min(mouseStart.Y, LevelEditor.MousePosition.Y);
                int w = Math.Max(mouseStart.X, LevelEditor.MousePosition.X) - x;
                int h = Math.Max(mouseStart.Y, LevelEditor.MousePosition.Y) - y;

                content.DrawRectangle(x, y, w, h, Microsoft.Xna.Framework.Color.Red * .1f);
                content.DrawLineAngle(x, y, w, Util.RIGHT, Microsoft.Xna.Framework.Color.Red);
                content.DrawLineAngle(x, y, h + 1, Util.DOWN, Microsoft.Xna.Framework.Color.Red);
                content.DrawLineAngle(x, y + h, w, Util.RIGHT, Microsoft.Xna.Framework.Color.Red);
                content.DrawLineAngle(x + w, y, h, Util.DOWN, Microsoft.Xna.Framework.Color.Red);
            }
        }

        public override void OnMouseLeftDown(System.Drawing.Point location)
        {
            drawing = true;
            mouseStart = location;
        }

        public override void OnMouseLeftUp(Point location)
        {
            drawing = false;

            int x = Math.Min(mouseStart.X, LevelEditor.MousePosition.X);
            int y = Math.Min(mouseStart.Y, LevelEditor.MousePosition.Y);
            int w = Math.Max(mouseStart.X, LevelEditor.MousePosition.X) - x;
            int h = Math.Max(mouseStart.Y, LevelEditor.MousePosition.Y) - y;
            Rectangle r = new Rectangle(x, y, w, h);

            List<Entity> hit = LayerEditor.Layer.Entities.FindAll(e => e.Bounds.IntersectsWith(r));
            if (hit.Count > 0)
                LevelEditor.Perform(new EntityRemoveAction(LayerEditor.Layer, hit));
        }
    }
}
