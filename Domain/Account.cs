using System;

namespace Lab3.Domain
{
    [Serializable]

    public class Account : Entity<long>
    {
        private string _username;
        private string _password;

        public Account(string _username, string password)
        {
            this._username = _username;
            this._password = password;
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}

