using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using System.Threading.Tasks;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using myShades.Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myShades.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private TimeSpan interval = new TimeSpan();
        private Storyboard myStoryboard;
        private DoubleAnimation myDoubleAnimation;
        Gradient test = new Gradient();

        Game game;

        public GamePage()
        {
            this.InitializeComponent();
            SetEvent();
            game = new Game(MainCanvas, ScoreBox,NextColor,5);
            //makeTest();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                game.setColor((int)e.Parameter);
            else
                game.setColor(4);
        }
        public async void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    game.moveCurentAside(-1);
                    break;
                case VirtualKey.Right:
                    game.moveCurentAside(1);
                    break;
                case VirtualKey.Down:
                    game.moveCurentDown();
                    game.moveCurentDown();
                    break;
                case VirtualKey.Up:
                    game.Swap();
                    break;
                case VirtualKey.Space:
                    if (game.Timer.IsEnabled)
                    {
                        game.Timer.Stop();
                    }
                    else
                    {
                        game.Timer.Start();
                    }
                    break;
                case VirtualKey.Escape:
                    game.getTable();
                    game.Timer.Stop();
                    MainPage.MainFrame.Navigate(typeof(MainMenue));
                    //DeleteEvent();
                    break;
            }
        }

        private void SetEvent()
        {
           Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            //Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
            //Window.Current.CoreWindow.PointerMoved += CoreWindow_PointerMoved;
            //Window.Current.CoreWindow.PointerReleased += CoreWindow_PointerReleased;
        }


        /*private void makeTest()
        {
            test.makeRed();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel1.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }

            test.makeGrin();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel2.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }

            test.makeBlue();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel3.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }

            test.makeYellow();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel4.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }

            test.makeViolet();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel5.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }

            test.makeLightBlue();
            test.setCurenGradient();
            for (int i = 0; i < 7; i++)
            {
                Panel6.Children.Add(new Rectangle()
                {
                    Fill = new SolidColorBrush(test.getShadesList()[i]),
                    Width = 50,
                    Height = 35
                });
            }
        }

        private void RangeBase_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if ((ScrollBar.Value) <= 255)
            {
                TestRect1.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    255,
                    Convert.ToByte(255 - ScrollBar.Value),
                    Convert.ToByte(255 - ScrollBar.Value)));
                TestRect2.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(255 - ScrollBar.Value),
                    255,
                    Convert.ToByte(255 - ScrollBar.Value)));
                TestRect3.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(255 - ScrollBar.Value),
                    Convert.ToByte(255 - ScrollBar.Value),
                    255));
                TestRect4.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    255,
                    255,
                    Convert.ToByte(255 - ScrollBar.Value)));
                TestRect5.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    255,
                    Convert.ToByte(255 - ScrollBar.Value),
                    255));
                TestRect6.Fill =
                    new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(255 - ScrollBar.Value),
                    255,
                    255));
            }
            else
            {
                TestRect1.Fill = new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(510 - ScrollBar.Value),
                    0,
                    0));
                TestRect2.Fill = new SolidColorBrush(Color.FromArgb(255,
                    0,
                    Convert.ToByte(510 - ScrollBar.Value),
                    0));
                TestRect3.Fill = new SolidColorBrush(Color.FromArgb(255,
                    0,
                    0,
                    Convert.ToByte(510 - ScrollBar.Value)));
                TestRect4.Fill = new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(510 - ScrollBar.Value),
                    Convert.ToByte(510 - ScrollBar.Value),
                    0));
                TestRect5.Fill = new SolidColorBrush(Color.FromArgb(255,
                    Convert.ToByte(510 - ScrollBar.Value),
                    0,
                    Convert.ToByte(510 - ScrollBar.Value)));
                TestRect6.Fill = new SolidColorBrush(Color.FromArgb(255,
                    0,
                    Convert.ToByte(510 - ScrollBar.Value),
                    Convert.ToByte(510 - ScrollBar.Value)));
            }
        }
        
        private void _1_OnClick(object sender, RoutedEventArgs e)
        {
            ScrollBar.Value -= 1;
        }

        private void _2_OnClick(object sender, RoutedEventArgs e)
        {
            ScrollBar.Value += 1;
        }
        */
    }
}
