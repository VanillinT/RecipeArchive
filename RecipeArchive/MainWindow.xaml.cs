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

namespace RecipeArchive
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<GeneralClass> Type = new List<GeneralClass>();
        List<GeneralClass.CookBook> CookingRecepies = new List<GeneralClass.CookBook>();
        List<GeneralClass.Games> Games = new List<GeneralClass.Games>();
        List<GeneralClass.Games.Minecraft> MinecraftRecepies = new List<GeneralClass.Games.Minecraft>();
        public MainWindow()
        {
            InitializeComponent();
            Functions.LoadData();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
