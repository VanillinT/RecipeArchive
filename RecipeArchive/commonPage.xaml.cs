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
    /// Логика взаимодействия для commonPage.xaml
    /// </summary>
    public partial class commonPage : Page
    {
        List<TextBox> _tblist = new List<TextBox>();
        public commonPage()
        {
            InitializeComponent();
            if (!LoginPage.IsLogged())
            {
                editButton.IsEnabled = false;
                saveButton.IsEnabled = false;
            }
            kindBox.Text = RecipePage.ChosenCommon().Kind;
            kindBox.IsReadOnly = true;
            nameBox.Text = RecipePage.ChosenCommon().Name;
            _tblist.Add(nameBox);
            timeBox.Text = RecipePage.ChosenCommon().Time;
            _tblist.Add(timeBox);
            recipeBox.Text = RecipePage.ChosenCommon().Text;
            _tblist.Add(recipeBox);
            linkBox.Text = RecipePage.ChosenCommon().Link;
            _tblist.Add(linkBox);
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
            foreach (TextBox tb in _tblist)
                tb.IsReadOnly = false;
            kindBox.IsReadOnly = false;
            List<string> kinds = new List<string>();
            foreach (var recipe in MainMenu.CommonList())
            {
                int met = 0;
                foreach (var kind in kinds)
                    if (kind == recipe.Kind)
                        met += 1;
                if (met == 0)
                    kinds.Add(recipe.Kind);
            }
            kindBox.ItemsSource = kinds;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            kindBox.ItemsSource = null;
            kindBox.IsReadOnly = true;
            DeleteSpaces(kindBox);

            foreach (TextBox tb in _tblist)
            {
                DeleteSpaces(tb);
                tb.IsReadOnly = true;
            }

            int _mistakes = 0;
            if (timeBox.Text != null)
            {
                try
                {
                    int.Parse(timeBox.Text);
                }
                catch
                {
                    _mistakes += 1;
                }
            }
            if (kindBox.Text == "")
                _mistakes += 1;
            if (nameBox.Text == "")
                _mistakes += 1;
            if (_mistakes == 0)
            {
                RecipePage.ChosenCommon().Kind = kindBox.Text;
                RecipePage.ChosenCommon().Name = nameBox.Text;
                RecipePage.ChosenCommon().Time = timeBox.Text;
                RecipePage.ChosenCommon().Link = linkBox.Text;
                RecipePage.ChosenCommon().Text = recipeBox.Text;
                MainMenu.CommonList()[RecipePage.IndexOfItem()] = RecipePage.ChosenCommon();
                if (LoginPage.IsLogged())
                    MainMenu.UsersList()[RecipePage.IndexOfItemU()] = RecipePage.ChosenCommon();
            }
            else
            {
                MessageBox.Show("Incorrect data was entered");
                kindBox.Text = RecipePage.ChosenCommon().Kind;
                nameBox.Text = RecipePage.ChosenCommon().Name;
                timeBox.Text = RecipePage.ChosenCommon().Time;
                recipeBox.Text = RecipePage.ChosenCommon().Text;
                linkBox.Text = RecipePage.ChosenCommon().Link;
            }
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                nameBox.Focus();
        }

        private void timeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                timeBox.Focus();
        }

        private void linkBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                linkBox.Focus();
        }
    }
}
