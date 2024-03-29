﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelEditors.Actions.EntityActions;
using System.Drawing;
using System.Diagnostics;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    public class EntityAddNodeTool : EntityTool
    {
        private bool moving;
        private Entity moveEntity;
        private int moveIndex;
        private EntityMoveNodeAction moveAction;

        public EntityAddNodeTool()
            : base("Add Node", "addNode.png")
        {

        }

        public override void OnMouseLeftDown(Point location)
        {
            Point node = LayerEditor.MouseSnapPosition;

            if (Ogmo.EntitySelectionWindow.Selected.Count == 1)
            {
                Entity e = Ogmo.EntitySelectionWindow.Selected[0];
                if (e.Definition.NodesDefinition.Enabled)
                {
                    if (e.Nodes.Contains(node))
                    {
                        moving = true;
                        moveEntity = e;
                        moveIndex = e.Nodes.FindIndex(p => p == node);
                    }
                    else if (e.Nodes.Count != e.Definition.NodesDefinition.Limit)
                    {
                        LevelEditor.Perform(new EntityAddNodeAction(LayerEditor.Layer, e, node));
                    }
                }
            }
            else
            {
                LevelEditor.StartBatch();
                foreach (var e in Ogmo.EntitySelectionWindow.Selected)
                {
                    if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(node))
                        LevelEditor.BatchPerform(new EntityAddNodeAction(LayerEditor.Layer, e, node));
                }
                LevelEditor.EndBatch();
            }
        }

        public override void OnMouseMove(Point location)
        {
            if (moving && LayerEditor.MouseSnapPosition != moveEntity.Nodes[moveIndex])
            {
                if (moveAction == null)
                {
                    moveAction = new EntityMoveNodeAction(LayerEditor.Layer, moveEntity, moveIndex, LayerEditor.MouseSnapPosition);
                    LevelEditor.Perform(moveAction);
                }
                else
                {
                    moveAction.DoAgain(LayerEditor.MouseSnapPosition);
                }
            }
        }

        public override void OnMouseLeftUp(Point location)
        {
            if (moving)
            {
                moving = false;
                moveAction = null;
            }
        }

        public override void OnMouseRightClick(Point location)
        {
            if (moving)
                return;

            Point node = LayerEditor.MouseSnapPosition;

            LevelEditor.StartBatch();
            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled)
                {
                    int index = e.Nodes.IndexOf(node);
                    if (index != -1)
                        LevelEditor.BatchPerform(new EntityRemoveNodeAction(LayerEditor.Layer, e, index));
                }
            }
            LevelEditor.EndBatch();
        }

        public override void Draw()
        {
            Point mouse = LayerEditor.MouseSnapPosition;

            foreach (var e in Ogmo.EntitySelectionWindow.Selected)
            {
                if (e.Definition.NodesDefinition.Enabled && e.Nodes.Count != e.Definition.NodesDefinition.Limit && !e.Nodes.Contains(mouse))
                {
                    //Draw the node ghost image
                    if (e.Definition.NodesDefinition.Ghost)
                    {
                        Point p = e.Position;
                        e.Position = mouse;
                        Ogmo.EditorDraw.DrawEntity(e, .25f);
                        e.Position = p;
                    }

                    //Draw the lines
                    if (e.Nodes.Count == 0 || e.Definition.NodesDefinition.DrawMode == Definitions.EntityNodesDefinition.PathMode.Fan)
                        Ogmo.EditorDraw.DrawLine(e.Position, mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);
                    else
                        Ogmo.EditorDraw.DrawLine(e.Nodes[e.Nodes.Count - 1], mouse, Microsoft.Xna.Framework.Color.Yellow * .5f);

                    //Draw the node itself
                    Ogmo.EditorDraw.DrawNode(mouse);
                }
            }
        }
    }
}
