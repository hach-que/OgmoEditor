﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class EnumValueDefinition : ValueDefinition
    {
        [XmlAttribute]
        public string[] Elements;

        public EnumValueDefinition()
            : base()
        {
            Elements = new string[] { "default" };
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new EnumValueEditor(this);
        }

        public override string ErrorCheck()
        {
            string s = base.ErrorCheck();
            return s;
        }

        public override ValueDefinition Clone()
        {
            EnumValueDefinition def = new EnumValueDefinition();
            def.Name = Name;
            def.Elements = (string[])Elements.Clone();
            return def;
        }
    }
}
