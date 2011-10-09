﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions.ValueDefinitions;
using System.Diagnostics;
using OgmoEditor.ProjectEditors.ValueEditors;

namespace OgmoEditor.ProjectEditors
{
    public partial class ValuesEditor : UserControl
    {
        private const string NEW_VALUE = "NewValue";

        private List<ValueDefinition> values;
        private UserControl valueEditor;

        public ValuesEditor()
        {
            InitializeComponent();
            values = new List<ValueDefinition>();
            valueEditor = null;

            //Initialize the type dropdown
            foreach (var s in ValueDefinition.VALUE_NAMES)
                typeComboBox.Items.Add(s);
        }

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        public List<ValueDefinition> Values
        {
            get { return values; }
            set
            {
                values = new List<ValueDefinition>(value);
                foreach (ValueDefinition v in values)
                    listBox.Items.Add(v.Name);
            }
        }

        private void setControlsFromValue(ValueDefinition v)
        {
            removeButton.Enabled = true;

            //Set the name
            nameTextBox.CausesValidation = true;
            nameTextBox.Enabled = true; 
            nameTextBox.Text = v.Name;

            //Set the type
            typeComboBox.CausesValidation = true;
            typeComboBox.Enabled = true;
            typeComboBox.SelectedIndex = ValueDefinition.VALUE_TYPES.FindIndex(e => e == v.GetType());

            //Remove the old value editor
            if (valueEditor != null)
                Controls.Remove(valueEditor);

            //Add the new one!
            valueEditor = v.GetEditor();
            if (valueEditor != null)
                Controls.Add(valueEditor);
        }

        private void disableControls()
        {
            removeButton.Enabled = false;

            nameTextBox.CausesValidation = false;
            nameTextBox.Enabled = false;        
            nameTextBox.Text = "";

            typeComboBox.CausesValidation = false;
            typeComboBox.Enabled = false;

            if (valueEditor != null)
            {
                Controls.Remove(valueEditor);
                valueEditor = null;
            }
        }

        private string getNewName()
        {
            int i = 0;
            string name;

            do
            {
                name = NEW_VALUE + i.ToString();
                i++;
            }
            while (values.Find(e => e.Name == name) != null);

            return name;
        }

        public string ErrorCheck(string container)
        {
            string s = "";

            //Check for duplicate value names
            List<string> found = new List<string>();
            foreach (ValueDefinition v in values)
            {
                if (v.Name != "" && !found.Contains(v.Name) && values.FindAll(e => e.Name == v.Name).Count > 1)
                {                  
                    s += ProjParse.Error(container + " contains multiple values with the name \"" + v.Name + "\"");
                    found.Add(v.Name);
                }
            }

            //Check for blank value names
            if (values.Find(e => e.Name == "") != null)
                s += ProjParse.Error(container + " contains value(s) with blank name");

            return s;
        }

        /*
         *  Events
         */
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
                setControlsFromValue(values[listBox.SelectedIndex]);
            else
                disableControls();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            IntValueDefinition v = new IntValueDefinition();
            v.Name = getNewName();
            values.Add(v);
            listBox.SelectedIndex = listBox.Items.Add(v.Name);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            values.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            listBox.SelectedIndex = Math.Min(listBox.Items.Count - 1, index);
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            values[listBox.SelectedIndex].Name = nameTextBox.Text;
            listBox.Items[listBox.SelectedIndex] = nameTextBox.Text;
        }

        private void typeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ValueDefinition oldDef = values[listBox.SelectedIndex];
            ValueDefinition newDef = null;

            for (int i = 0; i < ValueDefinition.VALUE_TYPES.Count; i++)
            {
                if (typeComboBox.SelectedIndex == i)
                {
                    newDef = (ValueDefinition)Activator.CreateInstance(ValueDefinition.VALUE_TYPES[i]);
                    break;
                }
            }

            newDef.Name = oldDef.Name;
            values[listBox.SelectedIndex] = newDef;
            setControlsFromValue(newDef);
        }
    }
}