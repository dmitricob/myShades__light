using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myShades.Pages
{   

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainMenue : Page
    {
        Color myColor;//= Colors.Green;
        Gradient myColors = new Gradient();
        private int ColorNumber;

        public MainMenue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                myColor = myColors.getAvailableColors()[(int)e.Parameter, 3];
                ColorNumber = (int)e.Parameter;
            }
            else
            {
                ColorNumber = 4;
                myColor = myColors.getAvailableColors()[4, 3];
            }
        }


        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //var myColor = Color.FromArgb(255, 0, 128, 0); //Colors.Green;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            (sender as TextBlock).Foreground = new SolidColorBrush(myColor);
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.Black);
        }


        private void NewGameButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPage.MainFrame.Navigate(typeof(GamePage));
            Frame.Navigate(typeof(GamePage), ColorNumber);
        }

        private void ContinueButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPage.MainFrame.Navigate(typeof(GamePage));
        }

        private void SetingsButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPage.MainFrame.Navigate(typeof(SettingsPage));
        }

        private void ExitButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
