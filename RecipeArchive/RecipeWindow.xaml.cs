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
    /// Логика взаимодействия для RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        public RecipeWindow()
        {
            InitializeComponent();
            if (MainMenu.ChosenIsCommon())
            {
                Height = 427;
                frame.Height = 390;
                frame.Navigate(new commonPage());
            }
            else
            {
                Height = 165;
                frame.Height = 150;
                frame.Navigate(new gamePage());
            }
        }
    }
}
