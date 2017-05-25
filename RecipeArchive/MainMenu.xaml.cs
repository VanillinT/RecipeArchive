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
        public static List<CommonRecipes> CommonList()
        { return _common; }
        static List<GameRecipes> _games = new List<GameRecipes>();
        public static List<GameRecipes> GameList()
        { return _games; }
        static List<CommonRecipes> _usercommon = new List<CommonRecipes>();
        public static List<CommonRecipes> UsersList()
        { return _usercommon; }

        DataContractJsonSerializer jsonFormatter4common = new DataContractJsonSerializer(typeof(List<CommonRecipes>));
        DataContractJsonSerializer jsonFormatter4games = new DataContractJsonSerializer(typeof(List<GameRecipes>));

        public MainMenu()
        {
            InitializeComponent();
            LoadData();
            if (!LoginPage.IsLogged())
                newrecipeButton.IsEnabled = false;
            else
                newrecipeButton.IsEnabled = true;
        }
        List<string> _commonkinds = new List<string>();
        List<string> _gamekinds = new List<string>();
                
        const string gameRecipes = "gameRecipes.txt";

        //methods
        public void LoadData()
        {
            commonBox.ItemsSource = null;
            gameBox.ItemsSource = null;
            _commonkinds.Clear();
            _gamekinds.Clear();
            if (LoginPage.IsLogged())
            {
                using (FileStream fs = new FileStream(LoginPage.FileName(), FileMode.OpenOrCreate, FileAccess.Read))
                {
                    if ((int)fs.Length > 0)
                    {
                        _usercommon = (List<CommonRecipes>)jsonFormatter4common.ReadObject(fs);
                    }
                }
            }

            using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if ((int)fs.Length > 0)
                {
                    _common = (List<CommonRecipes>)jsonFormatter4common.ReadObject(fs);
                }
            }

            using (FileStream fs = new FileStream(gameRecipes, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if ((int)fs.Length > 0)
                {
                    _games = (List<GameRecipes>)jsonFormatter4games.ReadObject(fs);
                }
                else
                    gameBox.ItemsSource = null;
            }

            if (LoginPage.IsLogged())
            {
                foreach (CommonRecipes recipe in _usercommon)
                {
                    int met = 0;
                    foreach (var kind in _commonkinds)
                        if (kind == recipe.Kind)
                            met += 1;
                    if (met == 0)
                        _commonkinds.Add(recipe.Kind);
                }
                commonBox.ItemsSource = _commonkinds;
            }
            else
            { 
                foreach (CommonRecipes recipe in _common)
                {
                    int met = 0;
                    foreach (var kind in _commonkinds)
                        if (kind == recipe.Kind)
                            met += 1;
                    if (met == 0)
                        _commonkinds.Add(recipe.Kind);
                }
                commonBox.ItemsSource = _commonkinds;
            }
            foreach (GameRecipes recipe in _games)
            {
                int met = 0;
                foreach (var kind in _gamekinds)
                    if (kind == recipe.Game)
                        met += 1;
                if (met == 0)
                    _gamekinds.Add(recipe.Game);
            }
            gameBox.ItemsSource = _gamekinds;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreatingWindow cw = new CreatingWindow();
            cw.Show();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        static string _chosenkind;
        public static string GetChosenKind()
        { return _chosenkind; }

        static bool _choseniscommon;
        public static bool ChosenIsCommon()
        { return _choseniscommon; }

        private void commonBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _chosenkind = commonBox.SelectedItem.ToString();
                _choseniscommon = true;
                NavigationService.Navigate(new RecipePage());
            }
            catch { }
        }

        private void gameBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _chosenkind = gameBox.SelectedItem.ToString();
                _choseniscommon = false;
                NavigationService.Navigate(new RecipePage());
            }
            catch { }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void commonBox_LostFocus(object sender, RoutedEventArgs e)
        {
            commonBox.SelectedItem = null;
        }

        private void gameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            gameBox.SelectedItem = null;
        }
    }
}
