using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.Tools;
using OgmoEditor.Definitions.LayerDefinitions;
using OgmoEditor.LevelEditors.Tools.GridTools;
using OgmoEditor.LevelEditors.Tools.EntityTools;
using OgmoEditor.LevelEditors.Tools.TileTools;
using OgmoEditor.Windows;
using System.ComponentModel;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using OgmoEditor.Protogame;

namespace OgmoPlugin.Protogame
{
    public class DebugWindow : OgmoWindow
    {
        private Timer c_AutoRefreshProperties;
        private IContainer components;
        private PropertyGrid c_DebugPropertyGrid;
    
        public DebugWindow()
            : base(HorizontalSnap.Left, VerticalSnap.Bottom)
        {
            Name = "DebugWindow";
            Text = "Debugging Information";
            ClientSize = new Size(280, 300);

            InitializeComponent();

            //this.c_DebugPropertyGrid.PropertyTabs.AddTabType(typeof(RemotePropertyViewer));
        }

        #region Windows Forms Designer

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c_DebugPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.c_AutoRefreshProperties = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // c_DebugPropertyGrid
            // 
            this.c_DebugPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_DebugPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.c_DebugPropertyGrid.Name = "c_DebugPropertyGrid";
            this.c_DebugPropertyGrid.Size = new System.Drawing.Size(284, 262);
            this.c_DebugPropertyGrid.TabIndex = 0;
            // 
            // c_AutoRefreshProperties
            // 
            this.c_AutoRefreshProperties.Enabled = true;
            this.c_AutoRefreshProperties.Tick += new System.EventHandler(this.c_AutoRefreshProperties_Tick);
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.c_DebugPropertyGrid);
            this.Name = "DebugWindow";
            this.ResumeLayout(false);

        }

        #endregion

        #region Property Editing

        private class DynamicPropertyDescriptor : PropertyDescriptor
        {
            private FocusableObject m_Target;
            private int m_PropertyIndex;

            public DynamicPropertyDescriptor(FocusableObject target, int index)
                : base((string)target.PropertyName(index), new Attribute[] { })
            {
                this.m_Target = target;
                this.m_PropertyIndex = index;
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get { return this.m_Target.GetType(); }
            }

            public override object GetValue(object component)
            {
                return this.m_Target.PropertyGet(this.m_PropertyIndex);
            }

            public override bool IsReadOnly
            {
                get { return !this.m_Target.PropertyHasSetter(this.m_PropertyIndex); }
            }

            public override Type PropertyType
            {
                get { return this.m_Target.PropertyType(this.m_PropertyIndex); }
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                this.m_Target.PropertySet(this.m_PropertyIndex, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            public override string Description
            {
                get
                {
                    return this.m_Target.PropertyDescription(this.m_PropertyIndex);
                }
            }

            public override string Category
            {
                get
                {
                    return this.m_Target.PropertyCategory(this.m_PropertyIndex);
                }
            }
        }

        private class RemotePropertyViewer : ICustomTypeDescriptor
        {
            private FocusableObject m_Target;

            public RemotePropertyViewer(FocusableObject target)
            {
                this.m_Target = target;
            }

            public FocusableObject Target
            {
                get
                {
                    return this.m_Target;
                }
            }

            #region ICustomTypeDescriptor Members

            public AttributeCollection GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this, true);
            }

            public string GetClassName()
            {
                return TypeDescriptor.GetClassName(this, true);
            }

            public string GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this, true);
            }

            public TypeConverter GetConverter()
            {
                return TypeDescriptor.GetConverter(this, true);
            }

            public EventDescriptor GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this, true);
            }

            public PropertyDescriptor GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this, true);
            }

            public object GetEditor(Type editorBaseType)
            {
                return TypeDescriptor.GetEditor(this, editorBaseType, true);
            }

            public EventDescriptorCollection GetEvents(Attribute[] attributes)
            {
                return TypeDescriptor.GetEvents(this, attributes, true);
            }

            public EventDescriptorCollection GetEvents()
            {
                return TypeDescriptor.GetEvents(this, true);
            }

            public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                return this.GetProperties();
            }

            public PropertyDescriptorCollection GetProperties()
            {
                PropertyDescriptorCollection props = new PropertyDescriptorCollection(null);
                for (int i = 0; i < this.m_Target.PropertyCount(); i++)
                    props.Add(new DynamicPropertyDescriptor(this.m_Target, i));
                return props;
            }

            public object GetPropertyOwner(PropertyDescriptor pd)
            {
                return this;
            }

            #endregion
        }

        #endregion

        public FocusableObject FocusedObject
        {
            get
            {
                if (this.c_DebugPropertyGrid.SelectedObject == null)
                    return null;
                else
                    return ((RemotePropertyViewer)this.c_DebugPropertyGrid.SelectedObject).Target;
            }
            set
            {
                if (value == null)
                    this.c_DebugPropertyGrid.SelectedObject = value;
                else if (this.c_DebugPropertyGrid.SelectedObject != null && ((RemotePropertyViewer)this.c_DebugPropertyGrid.SelectedObject).Target == value)
                    return; // do nothing; already up to date.
                else
                    this.c_DebugPropertyGrid.SelectedObject = new RemotePropertyViewer(value);
            }
        }

        private void c_AutoRefreshProperties_Tick(object sender, EventArgs e)
        {
            this.c_DebugPropertyGrid.Refresh();
        }
    }
}
