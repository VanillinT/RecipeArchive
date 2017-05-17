using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RecipeArchive
{
    class User
    {
        string _login;
        string _ecpassword;
        
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        
        public string ECPassword
        {
            get { return _ecpassword; }
            set { _ecpassword = value; }
        }

        public User(string login, string password)
        {
            _login = login;
            _ecpassword = password;
        }
    }
}
