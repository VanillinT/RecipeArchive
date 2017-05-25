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
    /// Логика взаимодействия для gamePage.xaml
    /// </summary>
    public partial class gamePage : Page
    {
        List<TextBox> _tblist = new List<TextBox>();
        public gamePage()
        {
            InitializeComponent();
            if (!LoginPage.IsLogged())
            {
                editButton.IsEnabled = false;
                saveButton.IsEnabled = false;
            }
            gameBox.Text = RecipePage.ChosenGame().Game;
            gameBox.IsReadOnly = true;
            nameBox.Text = RecipePage.ChosenGame().Name;
            _tblist.Add(nameBox);
            linkBox.Text = RecipePage.ChosenGame().Link;
            _tblist.Add(linkBox);
            ingBox.Text = RecipePage.ChosenGame().Ingredients;
            _tblist.Add(ingBox);
            foreach (var box in _tblist)
                box.IsReadOnly = true;
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
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            gameBox.IsReadOnly = false;
            foreach (var box in _tblist)
                box.IsReadOnly = false;
            List<string> kinds = new List<string>();
            foreach (var recipe in MainMenu.GameList())
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

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int _mistakes = 0;
            DeleteSpaces(gameBox);
            gameBox.ItemsSource = null;
            foreach (var box in _tblist)
                DeleteSpaces(box);
            if (gameBox.Text == "")
                _mistakes += 1;
            if (nameBox.Text == "")
                _mistakes += 1;
            if (ingBox.Text == "")
                _mistakes += 1;
            if(_mistakes == 0)
            {
                RecipePage.ChosenGame().Game = gameBox.Text;
                RecipePage.ChosenGame().Name = nameBox.Text;
                RecipePage.ChosenGame().Link = linkBox.Text;
                RecipePage.ChosenGame().Ingredients = ingBox.Text;
                MainMenu.GameList()[RecipePage.IndexOfItem()] = RecipePage.ChosenGame();
            }
            else
            {
                MessageBox.Show("Incorrect data was entered");
                gameBox.Text = RecipePage.ChosenGame().Game;
                nameBox.Text = RecipePage.ChosenGame().Name;
                linkBox.Text = RecipePage.ChosenGame().Link;
                ingBox.Text = RecipePage.ChosenGame().Ingredients;
            }
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                nameBox.Focus();
        }

        private void linkBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                linkBox.Focus();
        }

        private void ingBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ingBox.Focus();
        }
    }
}
