using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class Desktop: Category
    {
        PasswordCrypto passwordCrypto = new PasswordCrypto();
        public Desktop(string name, string username, string password, string userId)
        {
            this.name = name;
            this.username = username;
            this.password = passwordCrypto.encrypt(password);
            this.userId = userId;
        }

        //public void savePassword()
        //{
        //    Console.WriteLine("Saving Password "+this.name);
        //}
    }
}
