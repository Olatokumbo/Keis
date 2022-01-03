using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class User
    {
        public string userId;
        public string username;
        public bool isAdmin;

        public User(string userId, string username, bool isAdmin) {
            this.userId = userId;
            this.username = username;
            this.isAdmin = isAdmin;
        }

        public void logout()
        {
            this.userId = null;
            this.username = null;
            this.isAdmin = false;
        }
    }
}
