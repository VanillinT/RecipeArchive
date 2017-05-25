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
    /// Логика взаимодействия для RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {
        public RecipePage()
        {
            InitializeComponent();
            GetItems();
            if (!LoginPage.IsLogged())
                deletingButton.IsEnabled = false;
        }
        List<CommonRecipes> _commonlist = new List<CommonRecipes>();
        List<GameRecipes> _gamelist = new List<GameRecipes>();
        void GetItems()
        {
            if (MainMenu.ChosenIsCommon())
            {
                _commonlist.Clear();
                foreach (var item in MainMenu.CommonList())
                    if (item.Kind == MainMenu.GetChosenKind())
                        _commonlist.Add(item);
                itemBox.ItemsSource = _commonlist;
                comboBox.ItemsSource = _commonlist;
            }
            else
            {
                _gamelist.Clear();
                foreach (var item in MainMenu.GameList())
                    if (item.Game.ToLower() == MainMenu.GetChosenKind().ToLower())
                        _gamelist.Add(item);
                itemBox.ItemsSource = _gamelist;
                comboBox.ItemsSource = _gamelist;
            }
        }
        void Save()
        {
            if (MainMenu.ChosenIsCommon())
            {
                File.WriteAllText("Allcommon.txt", "");
                DataContractJsonSerializer jsonformatter = new DataContractJsonSerializer(typeof(List<CommonRecipes>));
                using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    jsonformatter.WriteObject(fs, MainMenu.CommonList());
                }
                if (LoginPage.IsLogged())
                {
                    File.WriteAllText(LoginPage.FileName(), "");
                    using (FileStream fs = new FileStream(LoginPage.FileName(), FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        jsonformatter.WriteObject(fs, MainMenu.UsersList());
                    }
                }
            }
            else
            {
                File.WriteAllText("gameRecipes.txt", "");
                DataContractJsonSerializer jsonformatter = new DataContractJsonSerializer(typeof(List<GameRecipes>));
                using (FileStream fs = new FileStream("gameRecipes.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    jsonformatter.WriteObject(fs, MainMenu.GameList());
                }

            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
            NavigationService.GoBack();
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainMenu.ChosenIsCommon())
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        List<CommonRecipes> _searchlist = new List<CommonRecipes>();
                        foreach (var item in _commonlist)
                            if (comboBox.Text == item.Name)
                            {
                                _searchlist.Add(item);
                                itemBox.ItemsSource = _searchlist;
                                break;
                            }
                        
                    }
                    catch
                    {
                        MessageBox.Show("Recipe is not found");
                        GetItems();
                    }
                }
                else if (comboBox.Text == "")
                    GetItems();
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    foreach (var item in _gamelist)
                        if (comboBox.Text != item.Name)
                            _gamelist.Remove(item);
                    itemBox.ItemsSource = _gamelist;
                }
                else if (comboBox.Text == "")
                    GetItems();
            }
        }

        static CommonRecipes _chosencommon;
        public static CommonRecipes ChosenCommon()
        { return _chosencommon; }
        static GameRecipes _chosengame;
        public static GameRecipes ChosenGame()
        { return _chosengame; }
        static int _indexofitemA;
        public static int IndexOfItem()
        { return _indexofitemA; }
        static int _indexofitemU;
        public static int IndexOfItemU()
        { return _indexofitemU; }
        private void itemBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainMenu.ChosenIsCommon())
            {
                try
                {
                    _chosencommon = itemBox.SelectedItem as CommonRecipes;
                    foreach (var item in MainMenu.CommonList())
                        if (_chosencommon == item)
                            _indexofitemA = MainMenu.CommonList().IndexOf(item);
                    if (LoginPage.IsLogged())
                    {
                        _chosencommon = itemBox.SelectedItem as CommonRecipes;
                        foreach (var item in MainMenu.UsersList())
                            if (_chosencommon == item)
                                _indexofitemU = MainMenu.UsersList().IndexOf(item);

                    }
                    RecipeWindow rw = new RecipeWindow();
                    rw.Show();
                }
                catch { }
            }
            else
            {
                try
                {
                    _chosengame = itemBox.SelectedItem as GameRecipes;
                    foreach (var item in MainMenu.GameList())
                        if (_chosengame == item)
                            _indexofitemA = MainMenu.GameList().IndexOf(item);
                    RecipeWindow rw = new RecipeWindow();
                    rw.Show();
                }
                catch { }
            }
        }

        private void deletingButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMenu.ChosenIsCommon())
            {
                if (itemBox.SelectedItem != null)
                    foreach (var item in MainMenu.CommonList())
                        if (itemBox.SelectedItem == item)
                        {
                            MainMenu.CommonList().Remove(item);
                            break;
                        }
             }
            else
            {
                if (itemBox.SelectedItem != null)
                    foreach (var item in MainMenu.GameList())
                        if (itemBox.SelectedItem == item)
                        {
                            MainMenu.GameList().Remove(item);
                            break;
                        }
            }
            Save();
            GetItems();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
            GetItems();
        }
    }
}
