using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RecipeArchive.GeneralClass;

namespace RecipeArchive
{
    class ProgramData
    {
        private List<CookBook> _cookbook;

        public List<CookBook> CookBook
        {
            get { return _cookbook; }
            set { _cookbook = value; }
        }

        private List<Games> _games;

        public List<Games> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        private List<Hookah> _hookah;

        public List<Hookah> Hookah
        {
            get { return _hookah; }
            set { _hookah = value; }
        }


    }
}
