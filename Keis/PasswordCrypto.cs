using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class PasswordCrypto
    {
        private int key = 1;

        public string encrypt(string password)
        {
            char[] passwordArray = new char[password.Length];
            //For lowercase letters
            for (int i = 0; i < password.Length; i++)
            {
                char ch = password[i];
                if (ch >= 'a' && ch <= 'z')
                {
                    ch = (char)(ch + key);
                    if (ch > 'z')
                    {
                        ch = (char)(ch - 'z' + 'a' - 1);
                    }
                    passwordArray[i] = ch;
                }
                //For Uppercase Letters
                else if (ch >= 'A' && ch <= 'Z')
                {
                    ch = (char)(ch + key);
                    if (ch > 'Z')
                    {
                        ch = (char)(ch - 'Z' + 'A' - 1);
                    }
                    passwordArray[i] = ch;
                }

                //For Numbers
                else if (ch >= '0' && ch <= '9')
                {
                    ch = (char)(ch + key);
                    if (ch > '9')
                    {
                        ch = (char)(ch - '9' + '0' - 1);
                    }
                    passwordArray[i] = ch;
                }


            }
            return new string(passwordArray);
        }
        public string decrypt(string password)
        {
            char[] passwordArray = new char[password.Length];
            //For lowercase letters
            for (int i = 0; i < password.Length; i++)
            {
                char ch = password[i];
                if (ch >= 'a' && ch <= 'z')
                {
                    ch = (char)(ch - key);
                    if (ch < 'a')
                    {
                        ch = (char)(ch + 'z' - 'a' + 1);
                    }
                    passwordArray[i] = ch;
                }
                //For Uppercase Letters
                else if (ch >= 'A' && ch <= 'Z')
                {
                    ch = (char)(ch - key);
                    if (ch < 'A')
                    {
                        ch = (char)(ch + 'Z' - 'A' + 1);
                    }
                    passwordArray[i] = ch;
                }

                //For Numbers
                else if (ch >= '0' && ch <= '9')
                {
                    ch = (char)(ch - key);
                    if (ch < '0')
                    {
                        ch = (char)(ch + '9' - '0' + 1);
                    }
                    passwordArray[i] = ch;
                }


            }
            return new string(passwordArray);
        }

    }
}
