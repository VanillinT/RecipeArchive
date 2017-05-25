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
    /// Логика взаимодействия для CommonWindow.xaml
    /// </summary>
    public partial class CommonWindow : Page
    {
        DataContractJsonSerializer jsonFormatter4common = new DataContractJsonSerializer(typeof(List<CommonRecipes>));

        public CommonWindow()
        {
            InitializeComponent();
            FillComboBox();
        }

        //methods
        void FillComboBox()
        {
            if (MainMenu.CommonList().Count > 0)
            {
                List<string> kinds = new List<string>();
                foreach (CommonRecipes recipe in MainMenu.CommonList())
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
        }

        void ClearAll()
        {
            kindBox.SelectedItem = null;
            nameBox.Clear();
            recipeBox.Clear();
            linkBox.Clear();
            timeBox.Clear();
        }

        void Save(List<CommonRecipes> _list, int where)
        {
            if (where == 1)
            {
                using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    jsonFormatter4common.WriteObject(fs, _list);
                }
            }
            if(where == 2)
            {
                using (FileStream fs = new FileStream(LoginPage.FileName(), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    jsonFormatter4common.WriteObject(fs, _list);
                }
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
            bool _allfine;
            int _mistakes = 0;
            if (kindBox.Text == "")
                _mistakes += 1;
            if (nameBox.Text == "")
                _mistakes += 1;
            if (recipeBox.Text == "")
                _mistakes += 1;
            if (_mistakes == 0)
                _allfine = true;
            else
                _allfine = false;
            return _allfine;
        }

        //events
        string _kind, _name, _link, _recipe, _time;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxes())
            {
                CommonRecipes _commonRecipe = new CommonRecipes(_kind, _name, _recipe, _link, _time);
                MainMenu.CommonList().Add(_commonRecipe);
                if (LoginPage.IsLogged())
                {
                    MainMenu.UsersList().Add(_commonRecipe);
                    Save(MainMenu.UsersList(), 2);
                }
                Save(MainMenu.CommonList(), 1);
                ClearAll();
                FillComboBox();
                _kind = _name = _recipe = _link = _time = null;
                // Pages.MainMenu.LoadData(); не работает, приходится использовать кнопку "Refresh" с точно таким же методом.
            }
            else
                MessageBox.Show("Not all fields are filled in.");
        }

        private void kindBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                _kind = kindBox.Text;
        }

        private void kindBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(kindBox);
            _kind = kindBox.Text;
        }

        private void timeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(timeBox);
            if (timeBox.Text != "")
            {
                try
                {
                    int.Parse(timeBox.Text);
                    _time = timeBox.Text;
                }
                catch
                {
                    MessageBox.Show("You can only use numbers for time");
                    timeBox.Clear();
                }
            }
        }

        private void recipeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(recipeBox);

            if (recipeBox.Text != "")
            {
                _recipe = recipeBox.Text;
            }
        }

        private void linkBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(linkBox);

            if (linkBox.Text != "")
            {
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
        }

        private void nameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(nameBox);

            if (nameBox.Text != "")
            {
                _name = nameBox.Text;
            }
        }
    }
}
