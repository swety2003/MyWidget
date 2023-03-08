using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PluginSDK.Controls
{
    [ContentProperty(name: "Content")]
    public class MyThumb : Thumb
    {
        private bool _locked;

        public bool Locked
        {
            get { return _locked; }
            set
            {
                _locked = value;
                if (value)
                {
                    this.IsHitTestVisible = false;
                    this.Background = new SolidColorBrush(Colors.Transparent); ;
                }
                else
                {
                    this.IsHitTestVisible = true;
                    this.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                }
            }
        }

        public Guid CardGuid => (Content as ICard).GUID;

        public bool SetLocked()
        {
            Locked = !Locked;
            return Locked;
        }
        public bool SetLocked(bool l)
        {
            Locked = l;
            return Locked;
        }

        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                // 在上一级父控件中没有找到指定名字的控件，就再往上一级找
                parent = VisualTreeHelper.GetParent(parent);
            }

            //return null;
            throw new InvalidOperationException();
        }


        Canvas GetCanvas()
        {
            return GetParentObject<Canvas>(this);
        }

        static MyThumb Preview = new MyThumb
        {
            Background = new SolidColorBrush(Color.FromArgb(32, 0, 0, 0)),
            BorderThickness = new Thickness(2),

            BorderBrush = (Brush)Application.Current.FindResource("Brush60"),

            //BorderBrush = new SolidColorBrush(Color.FromRgb(112, 112, 112)),
        };

        public delegate void CardMovedHandler(MyThumb sender, Point pos);

        public event CardMovedHandler? OnCardMoved;

        void CacPreview()
        {
            Preview.Height = this.Height;
            Preview.Width = this.Width;
            int col = Convert.ToInt32(Canvas.GetLeft(this) / unit);
            int row = Convert.ToInt32(Canvas.GetTop(this) / unit);

            Canvas.SetLeft(Preview, col * unit);
            Canvas.SetTop(Preview, row * unit);
        }

        public void MoveTo(double newX, double newY)
        {
            var top = Canvas.GetTop(this);
            var left = Canvas.GetLeft(this);
            DoubleAnimation anim1 = new DoubleAnimation(top, newY, TimeSpan.FromSeconds(0.1));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX, TimeSpan.FromSeconds(0.1));
            //anim1.FillBehavior = FillBehavior.Stop;
            //anim2.FillBehavior = FillBehavior.Stop;
            this.BeginAnimation(Canvas.LeftProperty, anim2);
            this.BeginAnimation(Canvas.TopProperty, anim1);


            //Canvas.SetLeft(this, newX);
            //Canvas.SetTop(this, newY);
        }

        static double unit = 48;
        // Create a new metadata instance with a modified default value.

        public MyThumb()
        {
            DragDelta += MyThumb_DragDelta;
            DragStarted += MyThumb_DragStarted;
            DragCompleted += MyThumb_DragCompleted;
            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);



        }




        private void MyThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {

            this.GetCanvas().Children.Remove(Preview);

            MoveTo(Canvas.GetLeft(Preview), Canvas.GetTop(Preview));

            this.Cursor = Cursors.Arrow;

            Panel.SetZIndex(this, 0);


            OnCardMoved?.Invoke(this, new Point(Canvas.GetLeft(Preview), Canvas.GetTop(Preview)));


        }

        private void MyThumb_DragStarted(object sender, DragStartedEventArgs e)
        {

            this.Cursor = Cursors.ScrollAll;

            Panel.SetZIndex(this, 10);

            CacPreview();

        }


        private void MyThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {



            if (!this.GetCanvas().Children.Contains(Preview))
            {
                this.GetCanvas().Children.Add(Preview);


                this.BeginAnimation(Canvas.LeftProperty, null);
                this.BeginAnimation(Canvas.TopProperty, null);

            }
            var thumb = sender as Thumb;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);

            CacPreview();
        }

        //static ControlTemplate Custom = new ControlTemplate { Template=  };

        static MyThumb()
        {






            HeightProperty.OverrideMetadata(typeof(MyThumb), new FrameworkPropertyMetadata(defaultValue: unit));
            WidthProperty.OverrideMetadata(typeof(MyThumb), new FrameworkPropertyMetadata(defaultValue: unit));

            //TemplateProperty.OverrideMetadata(typeof(MyThumb), new FrameworkPropertyMetadata(defaultValue: Custom));
        }



        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(MyThumb), new PropertyMetadata(null));




        public int HeightPix
        {
            get { return (int)GetValue(HeightPixProperty); }
            set { SetValue(HeightPixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightPix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightPixProperty =
            DependencyProperty.Register("HeightPix", typeof(int), typeof(MyThumb), new PropertyMetadata(1, (s, e) =>
            {
                var self = s as MyThumb;
                self.Height = (int)e.NewValue * unit;
            }));




        public int WidthPix
        {
            get { return (int)GetValue(WidthPixProperty); }
            set { SetValue(WidthPixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthPix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPixProperty =
            DependencyProperty.Register("WidthPix", typeof(int), typeof(MyThumb), new PropertyMetadata(1, (s, e) =>
            {
                var self = s as MyThumb;
                self.Width = (int)e.NewValue * unit;
            }));


    }
}
