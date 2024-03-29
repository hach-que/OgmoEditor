﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Drawing;
using OgmoEditor.Definitions;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors
{
    using Color = Microsoft.Xna.Framework.Color;
    using Rectangle = Microsoft.Xna.Framework.Rectangle; 

    /*
     *  EditorDraw class
     *      - singleton, instantiated by Ogmo.cs when the application starts
     *      - holds all the XNA data needed by the level/layer editors to draw, such as the SpriteBatch and GraphicsDevice
     *      - holds all loaded textures
     */
    public class EditorDraw
    {
        //Constant textures
        public Texture2D TexPixel { get; private set; }
        public Texture2D TexBG { get; private set; }
        public Texture2D TexLogo { get; private set; }

        //Project textures
        public Dictionary<EntityDefinition, Texture2D> EntityTextures { get; private set; }
        public Dictionary<Tileset, Texture2D> TilesetTextures { get; private set; }

        //Rendering stuff
        public GraphicsDevice GraphicsDevice { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public EditorDraw(GraphicsDevice device)
        {
            GraphicsDevice = device;
            SpriteBatch = new SpriteBatch(device);

            //Get all the standard textures set up
            TexPixel = Util.CreateRect(device, Color.White, 1, 1);
            TexBG = Read("bg.png");
            TexLogo = Read("logo.png");

            EntityTextures = new Dictionary<EntityDefinition, Texture2D>();
            TilesetTextures = new Dictionary<Tileset, Texture2D>();
        }

        public void LoadProjectTextures(Project project)
        {
            foreach (var kv in EntityTextures)
                kv.Value.Dispose();

            EntityTextures.Clear();
            TilesetTextures.Clear();

            foreach (EntityDefinition def in project.EntityDefinitions)
            {
                Texture2D tex = def.GenerateTexture(GraphicsDevice);
                if (tex != null)
                    EntityTextures.Add(def, tex);
            }

            foreach (Tileset def in project.Tilesets)
            {
                Texture2D tex = def.GenerateTexture(GraphicsDevice);
                TilesetTextures.Add(def, tex);
            }
        }

        /*
         *  Drawing helpers
         */
        public void DrawEntity(EntityDefinition definition, System.Drawing.Point position, float alpha = 1)
        {
            if (EntityTextures.ContainsKey(definition))
            {
                if (definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && definition.ImageDefinition.Tiled)
                {
                    Rectangle drawTo = Rectangle.Empty;
                    Texture2D texture = EntityTextures[definition];

                    for (drawTo.X = 0; drawTo.X < definition.Size.Width; drawTo.X += texture.Width)
                    {
                        drawTo.Width = Math.Min(texture.Width, definition.Size.Width - drawTo.X);
                        for (drawTo.Y = 0; drawTo.Y < definition.Size.Height; drawTo.Y += texture.Height)
                        {
                            drawTo.Height = Math.Min(texture.Height, definition.Size.Height - drawTo.Y);

                            SpriteBatch.Draw(texture, 
                                new Rectangle(drawTo.X + position.X, drawTo.Y + position.Y, drawTo.Width, drawTo.Height), 
                                new Rectangle(0, 0, drawTo.Width, drawTo.Height), 
                                Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
                        }
                    }
                }
                else
                    SpriteBatch.Draw(EntityTextures[definition],
                        new Rectangle(position.X, position.Y, definition.Size.Width, definition.Size.Height), null,
                        Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
            }
        }

        public void DrawEntity(Entity entity, float alpha = 1)
        {
            if (EntityTextures.ContainsKey(entity.Definition))
            {
                EntityDefinition definition = entity.Definition;
                System.Drawing.Point position = entity.Position;

                if (definition.ImageDefinition.DrawMode == EntityImageDefinition.DrawModes.Image && definition.ImageDefinition.Tiled)
                {
                    Rectangle drawTo = Rectangle.Empty;
                    Texture2D texture = EntityTextures[definition];

                    for (drawTo.X = 0; drawTo.X < entity.Size.Width; drawTo.X += texture.Width)
                    {
                        drawTo.Width = Math.Min(texture.Width, entity.Size.Width - drawTo.X);
                        for (drawTo.Y = 0; drawTo.Y < entity.Size.Height; drawTo.Y += texture.Height)
                        {
                            drawTo.Height = Math.Min(texture.Height, entity.Size.Height - drawTo.Y);

                            SpriteBatch.Draw(texture,
                                new Rectangle(drawTo.X + position.X, drawTo.Y + position.Y, drawTo.Width, drawTo.Height), 
                                new Rectangle(0, 0, drawTo.Width, drawTo.Height),
                                Color.White * alpha, 0, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
                        }
                    }
                }
                else
                    SpriteBatch.Draw(EntityTextures[definition], 
                        new Rectangle(position.X, position.Y, entity.Size.Width, entity.Size.Height), null,
                        Color.White * alpha, entity.Angle * Util.DEGTORAD, new Vector2(definition.Origin.X, definition.Origin.Y), SpriteEffects.None, 0);
            }
        }

        public void DrawNode(System.Drawing.Point node)
        {
            DrawRectangle(node.X, node.Y, 3, 3, Color.Red);
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawRectangle(int x, int y, int width, int height, Color color)
        {
            SpriteBatch.Draw(TexPixel, new Rectangle(x, y, width, height), color);
        }

        public void DrawRectangle(Rectangle rect, Color color, Vector2 origin, float angle)
        {
            SpriteBatch.Draw(TexPixel, rect, null, color, angle, origin, SpriteEffects.None, 0);
        }

        public void DrawHollowRect(System.Drawing.Rectangle rect, Color color)
        {
            DrawHollowRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawHollowRect(int x, int y, int width, int height, Color color)
        {
            DrawLineAngle(x, y, width, Util.RIGHT, color);
            DrawLineAngle(x, y, height + 1, Util.DOWN, color);
            DrawLineAngle(x, y + height, width, Util.RIGHT, color);
            DrawLineAngle(x + width, y, height, Util.DOWN, color);
        }

        public void DrawFillRect(System.Drawing.Rectangle rect, Color color)
        {
            DrawFillRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawFillRect(Rectangle rect, Color color)
        {
            DrawFillRect(rect.X, rect.Y, rect.Width, rect.Height, color);
        }

        public void DrawFillRect(int x, int y, int width, int height, Color color)
        {
            DrawHollowRect(x, y, width, height, color);
            DrawRectangle(x, y + 1, width - 1, height - 1, color * .3f);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            int length = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            float rotation = (float)Math.Atan2(y2 - y1, x2 - x1);

            DrawLineAngle(x1, y1, length, rotation, color);
        }

        public void DrawLine(System.Drawing.Point p1, System.Drawing.Point p2, Color color)
        {
            DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
        }

        public void DrawLine(Vector2 p1, Vector2 p2, Color color)
        {
            DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
        }

        public void DrawLineAngle(float x, float y, int length, float rotation, Color color)
        {
            SpriteBatch.Draw(TexPixel, new Vector2(x, y), null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        public void DrawGrid(Size grid, Size size, Color color)
        {
            for (int i = grid.Width; i < size.Width; i += grid.Width)
                DrawLineAngle(i, 2, size.Height - 4, Util.DOWN, color);

            for (int i = grid.Height; i < size.Height; i += grid.Height)
                DrawLineAngle(2, i, size.Width - 4, Util.RIGHT, color);
        }

        /*
         *  Helpers
         */
        private Texture2D Read(string filename)
        {
            FileStream s = new FileStream(Path.Combine(Ogmo.ProgramDirectory, "Content", filename), FileMode.Open);
            Texture2D tex = Texture2D.FromStream(GraphicsDevice, s);
            s.Close();

            return tex;
        }
    }
}
