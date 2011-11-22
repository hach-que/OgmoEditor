﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Diagnostics;

namespace OgmoEditor.ProjectEditors.LayerDefinitionEditors
{
    public partial class TileLayerDefinitionEditor : UserControl
    {
        private TileLayerDefinition def;

        public TileLayerDefinitionEditor(TileLayerDefinition def)
        {
            this.def = def;
            InitializeComponent();
            Location = new Point(206, 117);

            exportModeComboBox.SelectedIndex = (int)def.ExportMode;
            trimEmptyCheckBox.Checked = def.TrimEmpty;
            trimEmptyCheckBox.Enabled = def.ExportMode == TileLayerDefinition.TileExportMode.CSV;
        }

        private void exportModeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            def.ExportMode = (TileLayerDefinition.TileExportMode)exportModeComboBox.SelectedIndex;
            trimEmptyCheckBox.Enabled = def.ExportMode == TileLayerDefinition.TileExportMode.CSV;
        }

        private void trimEmptyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            def.TrimEmpty = trimEmptyCheckBox.Checked;
        }
    }
}
