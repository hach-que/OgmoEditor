﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueEditors;

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
        [XmlAttribute]
        public NumberUITypes UIType;

        public FloatValueDefinition()
            : base()
        {
            Default = 0;
            Min = float.MinValue;
            Max = float.MaxValue;
            Round = .1f;
            UIType = NumberUITypes.Field;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new FloatValueEditor(this);
        }
    }
}
