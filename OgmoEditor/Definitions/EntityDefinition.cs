﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using OgmoEditor.Definitions.ValueDefinitions;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor.Definitions
{
    public class EntityDefinition
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public bool ResizableX;
        [XmlAttribute]
        public bool ResizableY;
        [XmlAttribute]
        public bool Rotatable;
        [XmlAttribute]
        public float RotateIncrement;

        public Size Size;
        public Point Origin;
        public EntityImageDefinition ImageDefinition;
        public List<ValueDefinition> ValueDefinitions;
        public EntityNodesDefinition NodesDefinition;

        public EntityDefinition()
        {
            Limit = -1;
            Size = new Size(16, 16);
            RotateIncrement = 15;

            ValueDefinitions = new List<ValueDefinition>();

            ImageDefinition.ImagePath = "";
            ImageDefinition.RectColor = new OgmoColor(255, 0, 0);
            ImageDefinition.ClipRect = new Rectangle(0, 0, 16, 16);

            NodesDefinition.Limit = -1;
        }

        public EntityDefinition Clone()
        {
            EntityDefinition def = new EntityDefinition();
            def.Name = Name;
            def.Limit = Limit;
            def.ResizableX = ResizableX;
            def.ResizableY = ResizableY;
            def.Rotatable = Rotatable;
            def.RotateIncrement = RotateIncrement;
            def.Size = Size;
            def.Origin = Origin;
            def.ImageDefinition = ImageDefinition;
            def.ValueDefinitions = new List<ValueDefinition>();
            foreach (var d in ValueDefinitions)
                def.ValueDefinitions.Add(d.Clone());
            return def;
        }

        public Image GenerateButtonImage()
        {
            if (ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Rectangle)
            {
                //Draw a rectangle
                Bitmap b = new Bitmap(Size.Width, Size.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.FillRectangle(new SolidBrush(ImageDefinition.RectColor), new Rectangle(0, 0, Size.Width, Size.Height));
                }
                return (Image)b;
            }
            else if (ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                //Draw the image
                if (!File.Exists(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath)))
                    return null;
                else
                {
                    Image image = Image.FromFile(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath));
                    image = Util.CropImage(image, ImageDefinition.ClipRect);
                    return image;
                }
            }

            return null;
        }

        public Texture2D GenerateTexture(GraphicsDevice graphics)
        {
            if (ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image)
            {
                FileStream stream = new FileStream(Path.Combine(Ogmo.Project.SavedDirectory, ImageDefinition.ImagePath), FileMode.Open);
                Texture2D tex = Texture2D.FromStream(graphics, stream);                
                stream.Close();

                if (ImageDefinition.ClipRect.Width != tex.Width || ImageDefinition.ClipRect.Height != tex.Height || ImageDefinition.ClipRect.X != 0 || ImageDefinition.ClipRect.Y != 0)
                {
                    Texture2D texB = Util.CropTexture(tex, ImageDefinition.ClipRect);
                    tex.Dispose();
                    return texB;
                }
                else
                    return tex;
            }
            else
                return null;
        }

    }

    [XmlRoot("Image")]
    public struct EntityImageDefinition
    {
        public enum DrawModes { Rectangle, Image };

        [XmlAttribute]
        public DrawModes DrawMode;
        [XmlAttribute]
        public string ImagePath;
        [XmlAttribute]
        public bool Tiled;

        public OgmoColor RectColor;
        public Rectangle ClipRect;
    }

    [XmlRoot("Nodes")]
    public struct EntityNodesDefinition
    {
        public enum PathMode { None, Path, Circuit, Fan };

        [XmlAttribute]
        public bool Enabled;
        [XmlAttribute]
        public int Limit;
        [XmlAttribute]
        public PathMode DrawMode;
    }
}