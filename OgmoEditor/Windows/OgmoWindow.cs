﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace OgmoEditor.Windows
{
    public partial class OgmoWindow : Form
    {
        public enum HorizontalSnap { None, Left, Right };
        public enum VerticalSnap { None, Top, Bottom };

        private const int SNAP_RANGE = 20;

        private HorizontalSnap startHSnap;
        private VerticalSnap startVSnap;
        private HorizontalSnap hSnap;
        private VerticalSnap vSnap;
        private bool snapChange;

        public OgmoWindow(HorizontalSnap startHSnap = HorizontalSnap.None, VerticalSnap startVSnap = VerticalSnap.None)
        {
            this.startHSnap = startHSnap;
            this.startVSnap = startVSnap;
            InitializeComponent();

            Shown += onShown;
            Resize += enforceSnap;
            Ogmo.MainWindow.Resize += enforceSnap;
            Ogmo.MainWindow.LocationChanged += enforceSnap;       
        }

        private void onShown(object sender, EventArgs e)
        {
            hSnap = startHSnap;
            vSnap = startVSnap;
            enforceSnap();
        }

        private void OgmoWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
                Ogmo.MainWindow.Focus();
            }
        }

        private void OgmoWindow_LocationChanged(object sender, EventArgs e)
        {
            if (snapChange)
            {
                snapChange = false;
                return;
            }

            Rectangle r = Ogmo.MainWindow.EditBounds;
            Point p = Location;

            //Check for X snap
            if (Math.Abs(p.X - r.X) <= SNAP_RANGE)
            {
                hSnap = HorizontalSnap.Left;
                p.X = r.X;
            }
            else if (Math.Abs((p.X + Width) - (r.X + r.Width)) <= SNAP_RANGE)
            {
                hSnap = HorizontalSnap.Right;
                p.X = r.X + r.Width - Width;
            }
            else
                hSnap = HorizontalSnap.None;

            //Check for Y snap
            if (Math.Abs(p.Y - r.Y) <= SNAP_RANGE)
            {
                vSnap = VerticalSnap.Top;
                p.Y = r.Y;
            }
            else if (Math.Abs((p.Y + Height) - (r.Y + r.Height)) <= SNAP_RANGE)
            {
                vSnap = VerticalSnap.Bottom;
                p.Y = r.Y + r.Height - Height;
            }
            else
                vSnap = VerticalSnap.None;

            Location = p;
        }

        private void enforceSnap(object sender = null, EventArgs e = null)
        {
            Rectangle r = Ogmo.MainWindow.EditBounds;
            Point p = Location;

            //Check for X snap
            if (hSnap == HorizontalSnap.Left)
                p.X = r.X;
            else if (hSnap == HorizontalSnap.Right)
                p.X = r.X + r.Width - Width;

            //Check for Y snap
            if (vSnap == VerticalSnap.Top)
                p.Y = r.Y;
            else if (vSnap == VerticalSnap.Bottom)
                p.Y = r.Y + r.Height - Height;

            snapChange = true;
            Location = p;
        }
    }
}
