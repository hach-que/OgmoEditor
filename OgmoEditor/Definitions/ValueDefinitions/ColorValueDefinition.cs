﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    public class ColorValueDefinition : ValueDefinition
    {
        public OgmoColor Default;
        [XmlAttribute]
        public bool ExportAlphaChannel;

        public ColorValueDefinition()
            : base()
        {
            Default = new OgmoColor(255, 255, 255);
            ExportAlphaChannel = true;
        }

        public override System.Windows.Forms.UserControl GetEditor()
        {
            return new ColorValueEditor(this);
        }
    }
}
