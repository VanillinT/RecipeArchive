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
using System.Security.Cryptography;

namespace RecipeArchive
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<User> _users = new List<User>();
        public LoginPage()
        {
            InitializeComponent();
            LoadUsersData();
        }

        void LoadUsersData() //загрузка из файла Users.txt пар "логин-пароль(хэш)"
        {
            int i = 0;
            try
            {
                using (var sr = new StreamReader("Users.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split('%');
                        if (parts.Count() == 2)
                        {
                            string login = parts[0];
                            string ECpassword = parts[1];
                            var _user = new User(login, ECpassword);
                            _users.Add(_user);
                        }
                        else
                        {
                            line = null;
                            i += 1;
                            continue;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("При чтении из файла Users.txt была обнаружена ошибка.");
            }
            if (i > 0)
            {
                 MessageBox.Show("В ходе загрузки данных из файла было удалено строк с дефектом: {i}");
            }
        }
        void SaveData()
        {
            using (var sw = new StreamWriter("Users.txt"))
            {
                foreach (var user in _users)
                {
                    sw.WriteLine($"{user.Login}%{user.ECPassword}");
                }
            }
        }
        static string CalculateHash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString());
            }
            return sb.ToString();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                bool check = true;
                while (check)
                {
                    for (int i = 0; i < loginBox.Text.Length; i++)
                    {
                        if (loginBox.Text[i] == '%')
                        {
                            MessageBox.Show("В логине были использованы недопустимые символы.");
                            loginBox.Text = null;
                            break;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
                if (!check)
                {
                    var _newuser = new User(loginBox.Text, CalculateHash(passwordBox.Password));
                    _users.Add(_newuser);
                    SaveData();
                    loginBox.Text = null;
                    passwordBox.Password = null;
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля");
            }
        }
    }
}
