﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace OgmoEditor
{
    public struct OgmoColor
    {
        private const string REGEX32 = @"^(#|0x|)([0-9a-fA-F]{8})$";
        private const string REGEX24 = @"^(#|0x|)([0-9a-fA-F]{6})$";

        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public OgmoColor(Color color)
        {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public OgmoColor(byte r, byte g, byte b, byte a = 255)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public OgmoColor(string color)
        {
            if (IsValid32(color))
            {
                color = removePrefix(color);

                A = (byte)(Convert.ToByte(color[0]) * 16 + Convert.ToByte(color[1]));
                R = (byte)(Convert.ToByte(color[2]) * 16 + Convert.ToByte(color[3]));
                G = (byte)(Convert.ToByte(color[4]) * 16 + Convert.ToByte(color[5]));
                B = (byte)(Convert.ToByte(color[6]) * 16 + Convert.ToByte(color[7]));
            }
            else if (IsValid24(color))
            {
                color = removePrefix(color);

                A = 255;
                R = (byte)(Convert.ToByte(color[0]) * 16 + Convert.ToByte(color[1]));
                G = (byte)(Convert.ToByte(color[2]) * 16 + Convert.ToByte(color[3]));
                B = (byte)(Convert.ToByte(color[4]) * 16 + Convert.ToByte(color[5]));
            }
            else
                throw new Exception("String was not properly formatted to be converted to a color!");

        }

        static private string removePrefix(string from)
        {
            string color = from;

            if (color[0] == '#')
                color = color.Substring(1);
            else if (color[0] == '0' && color[1] == 'x')
                color = color.Substring(2);

            return color;
        }

        public override string ToString()
        {
            return A.ToString("X").PadLeft(2, '0') + R.ToString("X").PadLeft(2, '0') + G.ToString("X").PadLeft(2, '0') + B.ToString("X").PadLeft(2, '0');
        }

        public string ToString(bool alpha)
        {
            if (alpha)
                return ToString();
            else
                return R.ToString("X").PadLeft(2, '0') + G.ToString("X").PadLeft(2, '0') + B.ToString("X").PadLeft(2, '0');
        }

        static public bool IsValid24(string color)
        {
            return Regex.IsMatch(color, REGEX24);
        }

        static public bool IsValid32(string color)
        {
            return Regex.IsMatch(color, REGEX32);
        }

        /*
         *  Operators
         */
        static public implicit operator OgmoColor(string from)
        {
            return new OgmoColor(from);
        }

        static public implicit operator Color(OgmoColor from)
        {
            return Color.FromArgb(from.A, from.R, from.G, from.B);
        }

        static public explicit operator OgmoColor(Color from)
        {
            return new OgmoColor(from);
        }
    }
}