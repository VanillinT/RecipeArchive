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
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Page
    {
        DataContractJsonSerializer jsonFormatter4games = new DataContractJsonSerializer(typeof(List<GameRecipes>));
        public GameWindow()
        {
            InitializeComponent();
            FillComboBox();
        }

        //methods
        void FillComboBox()
        {
            if (MainMenu.GameList().Count > 0)
            {
                List<string> kinds = new List<string>();
                foreach (GameRecipes recipe in MainMenu.GameList())
                {
                    int met = 0;
                    foreach (var kind in kinds)
                        if (kind == recipe.Game)
                            met += 1;
                    if (met == 0)
                        kinds.Add(recipe.Game);
                }
                gameBox.ItemsSource = kinds;
            }
        }
        void ClearAll()
        {
            gameBox.SelectedItem = null;
            nameBox.Clear();
            linkBox.Clear();
            ingBlock.Text = null;
        }

        void Save(List<GameRecipes> _list)
        {
            using (FileStream fs = new FileStream("gameRecipes.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                jsonFormatter4games.WriteObject(fs, _list);
            }
        }

        void DeleteSpaces(TextBox box)
        {
            try
            {
                while (true)
                {
                    if (box.Text[0] == ' ')
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < box.Text.Length; i++)
                        {
                            sb.Append(box.Text[i]);
                        }
                        box.Text = sb.ToString();
                    }
                    else
                        break;
                }
                while (true)
                {
                    if (box.Text[box.Text.Length - 1] == ' ')
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < box.Text.Length - 1; i++)
                        {
                            sb.Append(box.Text[i]);
                        }
                        box.Text = sb.ToString();
                    }
                    else
                        break;
                }
            }
            catch { }
        }
        void DeleteSpaces(ComboBox box)
        {
            try
            {
                while (true)
                {
                    if (box.Text[0] == ' ')
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < box.Text.Length; i++)
                        {
                            sb.Append(box.Text[i]);
                        }
                        box.Text = sb.ToString();
                    }
                    else
                        break;
                }
                while (true)
                {
                    if (box.Text[box.Text.Length - 1] == ' ')
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < box.Text.Length - 1; i++)
                        {
                            sb.Append(box.Text[i]);
                        }
                        box.Text = sb.ToString();
                    }
                    else
                        break;
                }
            }
            catch { }
        }
        bool CheckBoxes()
         {
             int _mistakes = 0;
             bool _boxesarefine;
             if (string.IsNullOrWhiteSpace(gameBox.Text))
                 _mistakes += 1;
             if (string.IsNullOrWhiteSpace(nameBox.Text))
                 _mistakes += 1;
             if (string.IsNullOrWhiteSpace(ingBlock.Text))
                 _mistakes += 1;
            if (_mistakes == 0)
                _boxesarefine = true;
            else
                _boxesarefine = false;
             return _boxesarefine;
         }

        //events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ingBlock.Text == "")
                {
                    ingBlock.Text += $"{ingBox.Text}(x{int.Parse(amountBox.Text)})";
                    _ingredients = ingBlock.Text;
                }
                else
                {
                    ingBlock.Text += $" / {ingBox.Text}(x{int.Parse(amountBox.Text)})";
                    _ingredients += $" / {ingBox.Text}(x{int.Parse(amountBox.Text)})";
                }
                ingBox.Clear();
                amountBox.Clear();
            }
            catch
            {
                MessageBox.Show("Incorrect value was entered.");
            }
        }

        string _game, _itemname, _ingredients, _link;
        

        private void linkBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(linkBox);
            if(linkBox.Text != "")
            {
                _link = linkBox.Text;
            }
            int i = 0;
            foreach (char c in linkBox.Text)
            {
                if (c == '.')
                    i++;
            }
            if (i <= 2)
                _link = linkBox.Text;
            else
                linkBox.Clear();
        }

        private void ingBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(ingBox);
        }

        private void amountBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(amountBox);
        }

        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(nameBox);
            if(nameBox.Text != "")
            {
                _itemname = nameBox.Text;
            }
        }

        private void gameBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _game = gameBox.Text;
        }
        
        private void gameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(gameBox);
            _game = gameBox.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CheckBoxes())
            {
                GameRecipes _recipe = new GameRecipes(_game, _itemname, _ingredients, _link);
                MainMenu.GameList().Add(_recipe);
                Save(MainMenu.GameList());
                ClearAll();
                FillComboBox();
                _game = _itemname = _ingredients = _link = null;
                // Pages.MainMenu.LoadData(); не работает, приходится использовать кнопку "Refresh" с точно таким же методом.
            }
            else
                MessageBox.Show("Not all fields are filled in.");
        }
    }
}
