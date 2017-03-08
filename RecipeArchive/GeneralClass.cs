using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeArchive
{
    class GeneralClass
    {
        private string _kind;

        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        public class CookBook : GeneralClass
        {
            private string _nameofdish;

            public string NameOfDish
            {
                get { return _nameofdish; }
                set { _nameofdish = value; }
            }

            private string _recipe;

            public string Recipe
            {
                get { return _recipe; }
                set { _recipe = value; }
            }

            private int _timeofcookong;

            public int TimeOfCooking
            {
                get { return _timeofcookong; }
                set { _timeofcookong = value; }
            }
        }

        public class Games : GeneralClass
        {
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public class Minecraft : Games
            {
                private string _resourses;

                public string Resourses
                {
                    get { return _resourses; }
                    set { _resourses = value; }
                }

                private string _isused;

                public string IsUsed //(не)используемые предметы
                {
                    get { return _isused; }
                    set { _isused = value; }
                }

            }
        }
    }
}
