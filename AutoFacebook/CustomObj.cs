using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFacebook
{
    public class CustomObj
    {
        private List<Nick> _nicks;
        public List<Nick> Nicks
        {
            get { return _nicks; }
            set { _nicks = value; }
        }

        public CustomObj()
        {
            _nicks = new List<Nick>();
        }
    }

    public class Nick
    {
        //private string _id;
        //public string ID
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private CookieObj _cookie;
        public CookieObj Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        public Nick()
        {
            CookieObj _cookie = new CookieObj(); ;
            Cookie = _cookie;
            Password = "";
            Username = "";
        }
    }

    public class CookieObj
    {
        public CookieObj()
        {
            SB = "";
            FR = "";
            C_User = "";
            DATR = "";
            XS = "";
        }
        private string _sb;
        public string SB
        {
            get { return _sb; }
            set { _sb = value; }
        }

        private string _xs;
        public string XS
        {
            get { return _xs; }
            set { _xs = value; }
        }

        private string c_user;
        public string C_User
        {
            get { return c_user; }
            set { c_user = value; }
        }

        private string _fr;
        public string FR
        {
            get { return _fr; }
            set { _fr = value; }
        }

        private string _datr;
        public string DATR
        {
            get { return _datr; }
            set { _datr = value; }
        }
    }
}
