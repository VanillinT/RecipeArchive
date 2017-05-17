using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeArchive
{
    static class Pages
    {
        static LoginPage _loginPage = new LoginPage();
        public static LoginPage LoginPage
        {
            get
            {
                return _loginPage;
            }
        }
        static MainMenu _mainMenu = new MainMenu();
        public static MainMenu MainMenu
        {
            get
            {
                return _mainMenu;
            }
        }
        static GameWindow _gameWindow = new GameWindow();
        public static GameWindow GameWindow
        {
            get
            {
                return _gameWindow;
            }
        }
        static CommonWindow _commonWindow = new CommonWindow();
        public static CommonWindow CommonWindow
        {
            get
            {
                return _commonWindow;
            }
        }
    }
}
