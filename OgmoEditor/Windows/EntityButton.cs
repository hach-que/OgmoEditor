﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;

namespace OgmoEditor.Windows
{
    public partial class EntityButton : UserControl
    {
        static private readonly OgmoColor Selected = new OgmoColor(150, 220, 255);
        static private readonly OgmoColor NotSelected = new OgmoColor(255, 255, 255);

        public EntityDefinition Definition { get; private set; }

        public EntityButton(EntityDefinition definition, int x, int y)
        {
            Definition = definition;
            InitializeComponent();

            button.BackgroundImage = Definition.GenerateButtonImage();
            button.BackColor = (definition == Ogmo.EntitiesWindow.CurrentEntity) ? Selected : NotSelected;

            //Events
            Ogmo.EntitiesWindow.OnEntityChanged += onObjectChanged;
        }

        public void OnRemove()
        {

        }

        /*
         *  Events
         */
        private void button_Click(object sender, EventArgs e)
        {
            Ogmo.EntitiesWindow.SetObject(Definition);
        }

        private void onObjectChanged(EntityDefinition definition)
        {
            if (definition == Definition)
            {
                button.BackColor = Selected;
                Select();
            }
            else
                button.BackColor = NotSelected;
        }
    }
}
