﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.EntityActions
{
    public class EntityAddAction : EntityAction
    {
        private Entity added;
        private Entity removed;

        public EntityAddAction(EntityLayer entityLayer, Entity entity)
            : base(entityLayer)
        {
            added = entity;
        }

        public override void Do()
        {
            base.Do();

            //Enforce entity limit defined by the entity definition
            if (Ogmo.EntitiesWindow.CurrentEntity.Limit > 0 && EntityLayer.Entities.Count(e => e.Definition == Ogmo.EntitiesWindow.CurrentEntity) == Ogmo.EntitiesWindow.CurrentEntity.Limit)
                EntityLayer.Entities.Remove(removed = EntityLayer.Entities.Find(e => e.Definition == Ogmo.EntitiesWindow.CurrentEntity));

            //Place the entity
            EntityLayer.Entities.Add(added);

            //Add it to the selection
            if (Ogmo.LayersWindow.CurrentLayer == EntityLayer)
                Ogmo.EntitySelectionWindow.SetSelection(added);
        }

        public override void Undo()
        {
            base.Undo();

            //Remove the entity
            EntityLayer.Entities.Remove(added);

            //Re-add the one removed due to an entity limit
            if (removed != null)
                EntityLayer.Entities.Add(removed);

            //Remove it from the selection
            if (Ogmo.EntitySelectionWindow.IsSelected(added))
                Ogmo.EntitySelectionWindow.RemoveFromSelection(added);
        }
    }
}
