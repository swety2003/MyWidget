using MyWidgets.SDK;
using MyWidgets.SDK.Core.Card;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyWidgets.APP.Converters
{
    public class UIElement2RenderBitmap : IValueConverter
    {
        //https://stackoverflow.com/questions/14118003/snapshot-of-an-wpf-canvas-area-using-rendertargetbitmap
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {

                UIElement elt = ((IPreviewable)value).GetUIElement();

                //PresentationSource source = PresentationSource.FromVisual(elt);
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)elt.RenderSize.Width,
                      (int)elt.RenderSize.Height, 96, 96, PixelFormats.Default);

                VisualBrush sourceBrush = new VisualBrush(elt);
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                using (drawingContext)
                {
                    drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                          new Point(elt.RenderSize.Width, elt.RenderSize.Height)));
                }
                rtb.Render(drawingVisual);

                return rtb;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
