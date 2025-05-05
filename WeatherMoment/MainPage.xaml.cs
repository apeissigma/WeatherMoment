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
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page 
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        Program program = new Program();


        public MainPage()
        {
            InitializeComponent();
        }

        private void Start()
        {
            program.Start(); 
            SetImage();
            SetColors(); 
            DataContext = program;
        }

        private void SetImage()
        {
            string source = program.GetConditionImage();
            weatherImage.Source = new BitmapImage(new Uri(source, UriKind.Relative));
        }

        private void SetColors()
        {
            program.ForegroundColor = program.GetForegroundColor();
            program.BackgroundColor = program.GetBackgroundColor(); 
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void ChangeLocation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChangeLocation(program));
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Start(); 
        }

    }
}
