/* 
 * Credits:
 * Dynamic data binding and passing references on navigation (54-57)
 * * Code assistance by Prof. Janell Baxter
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WeatherMoment.Program;
using static WeatherMoment.Utility;

namespace WeatherMoment
{
    public partial class MainPage : Page 
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;

        public MainPage()
        {
            InitializeComponent();
        }

        public void Start()
        {
            window.program.Start(); 
            SetImage();
            SetColors(); 
            DataContext = window.program;
        }

        private void SetImage()
        {
            string source = window.program.GetConditionImage();
            weatherImage.Source = new BitmapImage(new Uri(source, UriKind.Relative));
        }

        private void SetColors()
        {
            window.program.ForegroundColor = window.program.GetForegroundColor();
            window.program.BackgroundColor = window.program.GetBackgroundColor(); 
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void ChangeLocation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeLocation(this));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Start(); 
        }
    }
}
