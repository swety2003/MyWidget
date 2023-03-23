using MaterialColorUtilities.Palettes;
using MaterialColorUtilities.Schemes;
using MaterialColorUtilities.Utils;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DColor = System.Drawing.Color;
namespace MyWidgets.SDK.Styles
{
    internal class ThemeUtil
    {

        private static uint[] CreateResizedImage(ImageSource source, int width=200, int height=200, int margin=0)
        {
            var rect = new Rect(margin, margin, width - margin * 2, height - margin * 2);

            var group = new DrawingGroup();
            RenderOptions.SetBitmapScalingMode(group, BitmapScalingMode.HighQuality);
            group.Children.Add(new ImageDrawing(source, rect));

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawDrawing(group);

            var resizedImage = new RenderTargetBitmap(
                width, height,         // Resized dimensions
                96, 96,                // Default DPI values
                PixelFormats.Default); // Default pixel format
            resizedImage.Render(drawingVisual);

            var img = BitmapFrame.Create(resizedImage);

            var arr = new uint[200*200*8];
            return arr;
        }


        public static Scheme<Color> Create(string source,bool isdark)
        {

            //uint[] pixels = CreateResizedImage(source);
            SKBitmap bitmap;
            using (FileStream fs = new FileStream(source, FileMode.Open))
            {

                bitmap = SKBitmap.Decode(fs).Resize(new SKImageInfo(112, 112), SKFilterQuality.Medium);
            }

            uint[] pixels = Array.ConvertAll(bitmap.Pixels, p => (uint)p);
            // This is where the magic happens
            uint seedColor = ImageUtils.ColorsFromImage(pixels).First();

            Console.WriteLine($"Seed: {StringUtils.HexFromArgb(seedColor)}");

            // CorePalette gives you access to every tone of the key colors
            CorePalette corePalette = CorePalette.Of(seedColor);

            // Map the core palette to color schemes
            // A Scheme contains the named colors, like Primary or OnTertiaryContainer
            Scheme<uint> lightScheme = new LightSchemeMapper().Map(corePalette);
            Scheme<uint> darkScheme = new DarkSchemeMapper().Map(corePalette);

            // Easily convert between Schemes with different color types
            //Scheme<DColor> lightSchemeColor = lightScheme.Convert(x => DColor.FromArgb((int)x));
            //Scheme<DColor> darkSchemeColor = darkScheme.Convert(x => DColor.FromArgb((int)x));

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
