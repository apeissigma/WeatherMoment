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
    /// <summary>
    /// Interaction logic for ChangeLocation.xaml
    /// </summary>
    public partial class ChangeLocation : Page
    {
        Program program; 


        public ChangeLocation(Program thisProgram)
        {
            InitializeComponent();
            program = thisProgram;
            DataContext = program; 
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string input = TextBox.Text; 
            if (int.TryParse(input, out int i))
            {
                if (i > 10000 && i < 99999 )
                {
                    program.Zip = i;
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
