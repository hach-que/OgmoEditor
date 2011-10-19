﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.ValueEditors
{
    public partial class ValueEditor : UserControl
    {
        public Value Value { get; private set; }

        public ValueEditor()
        {
            //Never call this!
        }

        public ValueEditor(Value value)
        {
            Value = value;
        }
    }
}
