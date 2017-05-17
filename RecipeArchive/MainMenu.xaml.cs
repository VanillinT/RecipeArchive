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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RecipeArchive
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        static List<CommonRecipes> _common = new List<CommonRecipes>();
        static List<GameRecipes> _games = new List<GameRecipes>();
        DataContractJsonSerializer jsonFormatter4common = new DataContractJsonSerializer(typeof(List<CommonRecipes>));
        DataContractJsonSerializer jsonFormatter4games = new DataContractJsonSerializer(typeof(List<GameRecipes>));
        public MainMenu()
        {
            InitializeComponent();
            LoadData();
            commonBox.ItemsSource = _common;
        }
       
        const string gameRecipes = "gameRecipes.txt";
        string filename = LoginPage.GetUsername() + "recipes.txt";
        public void Refresh()
        {
            commonBox.ItemsSource = null;
            gameBox.ItemsSource = null;
            LoadData();
            commonBox.ItemsSource = _common;
            gameBox.ItemsSource = _games;
        }
        void LoadData()
        {
            if (LoginPage.IsLogged())
            {
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if ((int)fs.Length > 0)
                    {
                        var readrecipes = (List<CommonRecipes>)jsonFormatter4common.ReadObject(fs);
                        foreach (var recipe in readrecipes)
                            _common.Add(recipe);
                    }
                }
            }
            else
            {
                using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if ((int)fs.Length > 0)
                    {
                        var readrecipes = (List<CommonRecipes>)jsonFormatter4common.ReadObject(fs);
                        foreach (var recipe in readrecipes)
                            _common.Add(recipe);
                    }
                }
            }
            using (FileStream fs = new FileStream(gameRecipes, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if ((int)fs.Length > 0)
                {
                    var readrecipes = (List<GameRecipes>)jsonFormatter4games.ReadObject(fs);
                    foreach (var recipe in readrecipes)
                        _games.Add(recipe);
                    gameBox.ItemsSource = _games;
                }
                else
                    gameBox.ItemsSource = null;
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreatingWindow cw = new CreatingWindow();
            cw.Show();
        }
    }
}
