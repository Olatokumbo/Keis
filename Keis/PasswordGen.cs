using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keis
{
    class PasswordGen
    {
        private int passwordLength = 10;
        private char[] characterList = { 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E',
                                         'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i',
                                         'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'O',
                                         'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's',
                                         'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X',
                                         'x', 'Y', 'y', 'Z', 'z', '0', '1', '2', '3',
                                         '4', '5', '6', '7', '8', '9' };

        public string generatePassword()
        {
            string randomPassword = "";
            Random rnd = new Random();
            for (int i = 0; i < passwordLength; i++)
            {
                int characterIndex = rnd.Next(0, characterList.Length);
                randomPassword += characterList[characterIndex];
            }
            return randomPassword;
        }
    }
}
