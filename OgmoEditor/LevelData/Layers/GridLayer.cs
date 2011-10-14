﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using System.Drawing;
using System.Diagnostics;

namespace OgmoEditor.LevelData.Layers
{
    public class GridLayer : Layer
    {
        public new GridLayerDefinition Definition { get; private set; }
        public bool[,] Grid;

        public GridLayer(GridLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;

            Grid = new bool[Ogmo.Project.LevelDefaultSize.Width / definition.Grid.Width, Ogmo.Project.LevelDefaultSize.Height / definition.Grid.Height];
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            XmlElement xml = doc.CreateElement(Definition.Name);

            if (Definition.ExportMode == GridLayerDefinition.ExportModes.Bitstring)
            {
                //Bitstring export
                string[] rows = new string[Grid.GetLength(1)];
                for (int i = 0; i < Grid.GetLength(1); i++)
                {
                    rows[i] = "";
                    for (int j = 0; j < Grid.GetLength(0); j++)
                        rows[i] += Grid[j, i] ? "1" : "0";
                }

                if (Definition.TrimZeroes)
                {
                    //Trim off trailing zeroes on rows and then trim trailing empty rows
                    for (int i = 0; i < rows.Length; i++)
                        rows[i] = rows[i].TrimEnd('0');

                    string s = string.Join("\n", rows, 0, rows.Length);
                    s = s.TrimEnd('\n');
                    xml.InnerText = s;
                }
                else
                    xml.InnerText = string.Join("\n", rows, 0, rows.Length);
            }
            else if (Definition.ExportMode == GridLayerDefinition.ExportModes.Rectangles)
            {
                //Rectangles export
                bool[,] copy = (bool[,])Grid.Clone();
                List<Rectangle> rects = new List<Rectangle>();

                //Create the rectangles
                Point p = getFirstCell(copy);
                while (p.X != -1)
                {
                    copy[p.X, p.Y] = false;
                    int w = 1;
                    int h = 1;

                    //Extend it to the right
                    while (p.X + w < copy.GetLength(0) && copy[p.X + w, p.Y])
                    {
                        copy[p.X + w, p.Y] = false;
                        w++;
                    }

                    //Extend it downward
                    while (p.Y + h < copy.GetLength(1))
                    {
                        for (int i = p.X; i < p.X + w; i++)
                            if (!copy[i, p.Y + h])
                                break;

                        for (int i = p.X; i < p.X + w; i++)
                            copy[i, p.Y + h] = false;
                        h++;
                    }

                    //Push the rectangle
                    rects.Add(new Rectangle(p.X, p.Y, w, h));

                    p = getFirstCell(copy);
                }

                //Export them as tags
                foreach (Rectangle r in rects)
                {
                    XmlElement rx = doc.CreateElement("rect");
                    XmlAttribute a;

                    a = doc.CreateAttribute("x");
                    a.InnerText = r.X.ToString();
                    rx.Attributes.Append(a);

                    a = doc.CreateAttribute("y");
                    a.InnerText = r.Y.ToString();
                    rx.Attributes.Append(a);

                    a = doc.CreateAttribute("w");
                    a.InnerText = r.Width.ToString();
                    rx.Attributes.Append(a);

                    a = doc.CreateAttribute("h");
                    a.InnerText = r.Height.ToString();
                    rx.Attributes.Append(a);

                    xml.AppendChild(rx);
                }
            }

            return xml;
        }

        private Point getFirstCell(bool[,] from)
        {
            for (int i = 0; i < from.GetLength(0); i++)
                for (int j = 0; j < from.GetLength(0); j++)
                    if (from[i, j])
                        return new Point(i, j);

            return new Point(-1, -1);
        }

        public override void SetXML(XmlElement xml)
        {
            
        }

        public override LayerEditor GetEditor(LevelEditors.LevelEditor editor)
        {
            return new GridLayerEditor(editor, this);
        }
    }
}
