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
        List<CommonRecipes> _common = new List<CommonRecipes>();

        public CommonWindow()
        {
            InitializeComponent();
            using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fs.Length > 0)
                {
                    var readrecipes = (List<CommonRecipes>)jsonFormatter4common.ReadObject(fs);
                    foreach (var recipe in readrecipes)
                        _common.Add(recipe);
                }
            }
            if (_common.Count > 0)
            {
                List<string> kinds = new List<string>();
                foreach (CommonRecipes rec in _common)
                    kinds.Add(rec.Kind);
                comboBox.ItemsSource = kinds;
            }
        }
        public void SaveData()
        {
            using (FileStream fs = new FileStream("AllCommon.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                jsonFormatter4common.WriteObject(fs, _common);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkBox.IsChecked = false;
            if (Pages.GameWindow == null)
                NavigationService.Navigate(new GameWindow());
            else
                NavigationService.Navigate(Pages.GameWindow);


        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool KBisOK=false, NBisOK, RBisOK, LBisOK, TBisOK;
            if (kindBox.Text != "")
            {
                int i = 0;
                foreach (char c in kindBox.Text)
                {
                    try
                    {
                        int.Parse(c.ToString());
                        i++;
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (i == 0)
                {
                    KBisOK = true;
                }
            }
            if (nameBox.Text == "")
            {
                NBisOK = false;
            }
            else
                NBisOK = true;
            if (recipeBox.Text == "")
            {
                RBisOK = false;
            }
            else
                RBisOK = true;
            if (linkBox.Text != "")
            {
                int i = 0;
                foreach (char c in linkBox.Text)
                {
                    if (c == '.')
                        i++;
                }
                if (i <= 2)
                    LBisOK = true;
                else
                    LBisOK = false;
            }
            else
                LBisOK = true;
            if (!LBisOK)
                MessageBox.Show("Make sure the link is right");
            try
            {
                int.Parse(timeBox.Text);
                TBisOK = true;
            }
            catch
            {
                TBisOK = false;
            }
            if (KBisOK && NBisOK && RBisOK && LBisOK && TBisOK)
            {
                CommonRecipes _commonRecipe = new CommonRecipes(kindBox.Text, nameBox.Text, recipeBox.Text, linkBox.Text, int.Parse(timeBox.Text));
                _common.Add(_commonRecipe);
                SaveData();
                kindBox.Clear();
                nameBox.Clear();
                recipeBox.Clear();
                linkBox.Clear();
                timeBox.Clear();
                SaveData();
                Pages.MainMenu.Refresh();
            }
            else
            {
                MessageBox.Show("Make sure everything is fine");
            }
        }
        
    }
}
