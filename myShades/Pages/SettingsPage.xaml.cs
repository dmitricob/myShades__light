using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myShades.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        Color myColor;
        int ColorNumber = 4;
        Gradient ColorList = new Gradient();
        public SettingsPage()
        {
            StackPanel panel;
            this.InitializeComponent();
            
            for (int i = 0; i < ColorList.getColorsCount(); i++)
            {
                ColorsComboBox.Items.Add(new StackPanel());
                panel = ColorsComboBox.Items[i] as StackPanel;
                panel.Orientation = Orientation.Horizontal;
                for (int j = 0; j < ColorList.getShadesCount(); j++)
                {
                    panel.Children.Add(new Rectangle()
                    {
                        Fill = new SolidColorBrush(ColorList.getAvailableColors()[i,j]),
                        Width = 20,
                        Height = 38
                    });
                }
            }
            ColorsComboBox.SelectedIndex=4;
            myColor = ColorList.getAvailableColors()[ColorsComboBox.SelectedIndex, 3];
            Debug.WriteLine("Constructor");
        }

        private void GoBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MainPage.MainFrame.GoBack();
            Frame.Navigate(typeof(MainMenue), ColorNumber);
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

        private void ColorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorNumber = ColorsComboBox.SelectedIndex;
            myColor = ColorList.getAvailableColors()[ColorNumber, 3];
        }

        public int selectedColor()
        {
            return ColorNumber;
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MainPage.MainFrame.GoBack();
        //}
    }
}