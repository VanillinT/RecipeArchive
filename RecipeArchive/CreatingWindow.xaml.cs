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
using System.Windows.Shapes;

namespace RecipeArchive
{
    /// <summary>
    /// Логика взаимодействия для CreatingWindow.xaml
    /// </summary>
    public partial class CreatingWindow : Window
    {
        public CreatingWindow()
        {
            InitializeComponent();
            frame.Navigate(new CommonWindow());
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Pages.GameWindow == null)
                    frame.Navigate(new GameWindow());
                else
                    frame.Navigate(Pages.GameWindow);
            }
            catch
            {
                frame.Navigate(new GameWindow());
            }
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
            frame.Navigate(Pages.CommonWindow);
            }
            catch
            {
                frame.Navigate(new CommonWindow());
            }
        }
        
    }
}
