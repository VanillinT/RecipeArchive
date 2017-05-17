using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RecipeArchive
{
    [DataContract]
    public class CommonRecipes
    {
        private string _kind;

        [DataMember]
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _text;

        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private int _time;

        [DataMember]
        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private string _link;

        [DataMember]
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public CommonRecipes(string k, string n, string s, string l, int t)
        {
            _kind = k;
            _name = n;
            _text = s;
            _link = l;
            _time = t;
        }

    }
    [DataContract]
    public class GameRecipes
    {
        private string _game;

        [DataMember]
        public string Game
        {
            get { return _game; }
            set { _game = value; }
        }

        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _ingred;

        [DataMember]
        public string Ingred
        {
            get { return _ingred; }
            set { _ingred = value; }
        }

        private string _link;

        [DataMember]
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public GameRecipes(string g, string n, string ing, string l)
        {
            _game = g;
            _name = n;
            _ingred = ing;
            _link = l;
        }

    }

}
