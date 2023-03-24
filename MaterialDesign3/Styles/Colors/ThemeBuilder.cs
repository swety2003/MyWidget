using MaterialColorUtilities.Palettes;
using MaterialColorUtilities.Schemes;
using MaterialColorUtilities.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ColorConverter = System.Windows.Media.ColorConverter;
using Color = System.Windows.Media.Color;

namespace MaterialDesign3.Styles.Colors
{
    public static class ThemeBuilder
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SystemParametersInfo(uint uAction, uint uParam, StringBuilder lpvParam, uint init);
        const uint SPI_GETDESKWALLPAPER = 0x0073;


        public static Bitmap ResizeImage(Image imgToResize, System.Drawing.Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        public static int[] GetPixels(this Bitmap img)
        {
            var ret = new List<int>();
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    ret.Add(img.GetPixel(i, j).ToArgb());
                }
            }
            return ret.ToArray();
        }

        public static Scheme<Color> Create(bool isdark)
        {
            string source = "";
            StringBuilder wallPaperPath = new StringBuilder(200);
            if (SystemParametersInfo(SPI_GETDESKWALLPAPER, 200, wallPaperPath, 0))
            {
                source = wallPaperPath.ToString();
            }
            else
            {
                throw new Exception();
            }


            Bitmap bitmap;
            using (FileStream fs = new FileStream(source, FileMode.Open))
            {
                bitmap = ResizeImage(Image.FromStream(fs), new Size(112, 112));
            }
            uint[] pixels = Array.ConvertAll(bitmap.GetPixels(), p => (uint)p);
            // This is where the magic happens
            uint seedColor = ImageUtils.ColorsFromImage(pixels).First();


            // CorePalette gives you access to every tone of the key colors
            CorePalette corePalette = CorePalette.Of(seedColor);

            // Map the core palette to color schemes
            // A Scheme contains the named colors, like Primary or OnTertiaryContainer
            Scheme<uint> lightScheme = new LightSchemeMapper().Map(corePalette);
            Scheme<uint> darkScheme = new DarkSchemeMapper().Map(corePalette);


            Scheme<Color> darkSchemeColor = darkScheme.Convert(x => (Color)ColorConverter.ConvertFromString("#" + x.ToString("X")[2..]));
            Scheme<Color> lightSchemeColor = lightScheme.Convert(x => (Color)ColorConverter.ConvertFromString("#" + x.ToString("X")[2..]));

            if (isdark)
            {
                return darkSchemeColor;
            }
            else
            {

                return lightSchemeColor;
            }


            //Scheme<string> lightSchemeString = lightScheme.Convert(x => "#" + x.ToString("X")[2..]);
            //Console.WriteLine("Light scheme", lightSchemeString);

            //Scheme<string> darkSchemeString = darkScheme.Convert(StringUtils.HexFromArgb);
            //Console.WriteLine("Dark scheme", darkSchemeString);


        }
    }
}
