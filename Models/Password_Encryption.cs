using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJCollegeMVC.Models
{
    public class Password_Encryption
    {
        public string Encryption(string Encrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(Encrypted.ToString());
            return Convert.ToBase64String(b);
        }
        public string Decryption(string Decrypted)
        {
            byte[] b;
            b = Convert.FromBase64String(Decrypted);
            Decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);

            return Decrypted;
        }
    }
}