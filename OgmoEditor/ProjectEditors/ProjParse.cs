﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OgmoEditor.ProjectEditors
{
    static class ProjParse
    {
        static public void GetInt(ref int to, TextBox box)
        {
            try
            {
                to = Convert.ToInt32(box.Text);
            }
            catch
            {
                box.Text = to.ToString();
            }
        }

        static public void GetSize(ref Size to, TextBox x, TextBox y)
        {
            try
            {
                to.Width = Convert.ToInt32(x.Text);
            }
            catch
            {
                x.Text = to.Width.ToString();
            }

            try
            {
                to.Height = Convert.ToInt32(y.Text);
            }
            catch
            {
                y.Text = to.Height.ToString();
            }
        }
    }
}