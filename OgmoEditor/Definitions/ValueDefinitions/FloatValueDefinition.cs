﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueDefinitionEditors;
using OgmoEditor.LevelEditors.ValueEditors;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.LevelValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlRoot("float")]
    public class FloatValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public float Default;
        [XmlAttribute]
        public float Min;
        [XmlAttribute]
        public float Max;
        [XmlAttribute]
        public float Round;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = 0;
            Max = 100;
            Round = .1f;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new FloatValueDefinitionEditor(this);
        }

        public override ValueEditor GetInstanceEditor(Value instance, int x, int y)
        {
            return new FloatValueEditor(instance, x, y);
        }

        public override ValueEditor GetInstanceLevelEditor(Value instance, int x, int y)
        {
            return new LevelFloatValueEditor(instance, x, y);
        }

        public override ValueDefinition Clone()
        {
            FloatValueDefinition def = new FloatValueDefinition();
            def.Name = Name;
            def.Default = Default;
            def.Min = Min;
            def.Max = Max;
            def.Round = Round;
            return def;
        }

        public override string GetDefault()
        {
            return Default.ToString();
        }

        public override string ToString()
        {
            return Name + " (float)";
        }
    }
}
