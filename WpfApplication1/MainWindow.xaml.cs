using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Int64 id = 0;
        private static Random random = new Random();
        private static Double x;
        private static Double y;

        private Canvas myPanel;

        public MainWindow()
        {
            InitializeComponent();

            myPanel = new Canvas();
            this.Content = myPanel;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
            Left = 0.0;
            Top = 0.0;
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            x = Width;
            y = Height;
            var style = Resources["ImageStyle"] as Style;

            var initImage = GenerateImage();
            initImage.Style = style;
            SetPosition(initImage);

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (tsender, te) => {
                var image = GenerateImage();
                image.Style = style;
                SetPosition(image);
            };
            timer.Start();
        }

        private void SetPosition(UIElement e)
        {
            myPanel.Children.Add(e);
            Canvas.SetBottom(e, random.NextDouble() * y);
            Canvas.SetLeft(e, random.NextDouble() * x);
        }

        private Image GenerateImage()
        {
            Image myImage = new Image();
            myImage.Name = "myImage" + Interlocked.Increment(ref id);
            this.RegisterName(myImage.Name, myImage);

            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"firefox.png", UriKind.Relative);
            myBitmapImage.DecodePixelWidth = 200;
            myBitmapImage.EndInit();
            myImage.Source = myBitmapImage;

            //DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            //myDoubleAnimation.From = 0.0;
            //myDoubleAnimation.To = 1.0;
            //myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(4));

            //var myStoryboard = new Storyboard();
            //myStoryboard.Children.Add(myDoubleAnimation);
            //Storyboard.SetTargetName(myDoubleAnimation, myImage.Name);
            //Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));

            //myImage.Loaded += (sender, e) =>
            //{
            //    myStoryboard.Begin(this);
            //};

            return myImage;
        }
    }
}
