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
        const string _usersfilename = "Users.txt";
        public LoginPage()
        {
            InitializeComponent();
            LoadUsersData();
            loginBox.Focus();
        }
        //methods
        static bool _logged = false;
        public static bool IsLogged()
        { return _logged; }

        static string _username;
        public static string Username()
        { return _username; }

        static string _filename;
        public static string FileName()
        { return _filename; }

        void LoadUsersData()
        {
            int i = 0;
            try
            {
                using (var sr = new StreamReader(_usersfilename))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split('%');
                        if (parts.Count() == 2)
                        {
                            string login = parts[0].ToLower();
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
                MessageBox.Show("An error appeared while reading Users.txt");
            }
            if (i > 0)
            {
                 MessageBox.Show("When loading, strings with an error were deleted: {0}.", i.ToString());
            }
        }
        void SaveData()
        {
            using (var sw = new StreamWriter(_usersfilename))
            {
                foreach (var user in _users)
                {
                    sw.WriteLine($"{user.Login.ToLower()}%{user.ECPassword}");
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
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
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
        //events
        private void Register_Click(object sender, RoutedEventArgs e)
        {

            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                bool _check = false;
                    for (int i = 0; i < loginBox.Text.Length; i++)
                    {
                        if (loginBox.Text[i] == '%' || loginBox.Text[i] == ' ')
                        {
                            MessageBox.Show("Invalid characters were used");
                            loginBox.Text = null;
                            _check = false;
                            break;
                        }
                        else
                        {
                            _check = true;
                        }
                    }
                if (_check)
                {
                    var _newuser = new User(loginBox.Text, CalculateHash(passwordBox.Password));
                    _users.Add(_newuser);
                    SaveData();
                    _username = loginBox.Text;
                    _filename = _username + "recipes.txt";
                    loginBox.Text = null;
                    passwordBox.Password = null;
                    _logged = true;
                    MessageBox.Show("New user was succesfully created.");
                    NavigationService.Navigate(new MainMenu());
                }
            }
            else
            {
                MessageBox.Show("Not all fields are filled in.");
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {

            bool _check = false;
            if (loginBox.Text != "" && passwordBox.Password != "")
            {
                string hash = CalculateHash(passwordBox.Password);
                for (int i = 0; i < _users.Count; i++)
                {
                    if (_users[i].Login == loginBox.Text && _users[i].ECPassword == hash)
                        _check = true;
                    else
                        continue;
                }
                if (_check)
                {
                    _username = loginBox.Text;
                    _filename = _username + "recipes.txt";
                    _logged = true;
                    NavigationService.Navigate(new MainMenu());
                }
                else
                    MessageBox.Show("Incorrect credentials entered.");
            }
            else
                MessageBox.Show("Not all fields are filled in.");

        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            _logged = false;
            NavigationService.Navigate(new MainMenu());
        }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            DeleteSpaces(loginBox);
        }

        private void loginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                passwordBox.Focus();
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login_Click(sender, e);
        }
    }
}
