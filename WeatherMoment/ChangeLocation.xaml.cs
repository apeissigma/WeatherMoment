/* 
 * Credits:
 * Dynamic data binding and passing references on navigation (25-37, 47-48)
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

namespace WeatherMoment
{
    public partial class ChangeLocation : Page
    {
        MainWindow window = (MainWindow)Application.Current.MainWindow;
        private MainPage _mainPage; 

        public ChangeLocation()
        {
            InitializeComponent();
            DataContext = window.program; 
        }

        public ChangeLocation(MainPage mainPage) : this()
        {
            _mainPage = mainPage; 
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string input = TextBox.Text; 
            if (int.TryParse(input, out int i))
            {
                if (i > 10000 && i < 99999 )
                {
                    window.program.Zip = i;
                    window.DataContext = window.program;
                    _mainPage.Start();  
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBlock.Text = "Please enter a valid 5-digit zip code.";
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
