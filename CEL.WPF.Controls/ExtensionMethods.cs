using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace CEL.WPF.Controls
{
    public static class ExtentionMethods
    {
        /// <summary>
        /// Get the required height and width of the specified text. Uses FortammedText
        /// </summary>
        public static Size MeasureTextSize(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize)
        {
            FormattedText ft = new System.Windows.Media.FormattedText(text,
                                                 System.Globalization.CultureInfo.CurrentCulture,
                                                 FlowDirection.LeftToRight,
                                                 new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                                                 fontSize,
                                                 Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }

        /// <summary>
        /// Get the required height and width of the specified text. Uses Glyph's
        /// </summary>
        public static Size MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize)
        {
            Typeface typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
            GlyphTypeface glyphTypeface;

            if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
            {
                return ExtentionMethods.MeasureTextSize(text, fontFamily, fontStyle, fontWeight, fontStretch, fontSize);
            }

            double totalWidth = 0;
            double height = 0;

            for (int n = 0; n < text.Length; n++)
            {
                ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];

                double width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;

                double glyphHeight = glyphTypeface.AdvanceHeights[glyphIndex] * fontSize;

                if (glyphHeight > height)
                {
                    height = glyphHeight;
                }

                totalWidth += width;
            }

            return new Size(totalWidth, height);
        }
        public static System.Windows.Media.Brush ToControlPaintDark(System.Windows.Media.Color mediaColor)
        {
            SolidColorBrush solidBrush = new SolidColorBrush(mediaColor);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Dark(color);
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFrom(System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B));

            return brush;
        }
        public static System.Windows.Media.Brush ToControlPaintDark(System.Windows.Media.Brush br)
        {
            SolidColorBrush solidBrush = (SolidColorBrush)br;
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Dark(color);
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFrom(System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B));

            return brush;
        }
        public static System.Windows.Media.Color ToControlPaintDarkColor(System.Windows.Media.Brush br)
        {
            SolidColorBrush solidBrush = (SolidColorBrush)br;
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Dark(color);

            return System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B);
        }
        public static System.Windows.Media.Color ToControlPaintDarkColor(System.Windows.Media.Color mediaColor,float percOfDarkDark)
        {
            SolidColorBrush solidBrush = new SolidColorBrush(mediaColor);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Dark(color, percOfDarkDark);

            return System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B);
        }
        public static System.Windows.Media.Color ToControlPaintDarkColor(System.Windows.Media.Color mediaColor)
        {
            SolidColorBrush solidBrush = new SolidColorBrush(mediaColor);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Dark(color);

            return System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B);
        }
        public static System.Windows.Media.Color ToControlPaintLightColor(System.Windows.Media.Color mediaColor, float percOfDarkDark)
        {
            SolidColorBrush solidBrush = new SolidColorBrush(mediaColor);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Light(color, percOfDarkDark);

            return System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B);
        }
        public static System.Windows.Media.Color ToControlPaintLightColor(System.Windows.Media.Color mediaColor)
        {
            SolidColorBrush solidBrush = new SolidColorBrush(mediaColor);
            System.Drawing.Color color = System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);

            System.Drawing.Color darkColor = System.Windows.Forms.ControlPaint.Light(color);

            return System.Windows.Media.Color.FromArgb(darkColor.A, darkColor.R, darkColor.G, darkColor.B);
        }
        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color originalColor)
        {
            return System.Windows.Media.Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, originalColor.B);
        }

        public static System.Drawing.Color ToDrawingColor(System.Windows.Media.SolidColorBrush br)
        {
            return System.Drawing.Color.FromArgb(
                br.Color.A,
                br.Color.R,
                br.Color.G,
                br.Color.B);
        }
        public static System.Drawing.Color ToDrawingColor(System.Windows.Media.Brush br)
        {
            //SolidColorBrush brushColor = new SolidColorBrush(br);

            SolidColorBrush solidBrush = (SolidColorBrush)br;
            return System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);
        }
    }
}
