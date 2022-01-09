using Blog.Core.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Blog.Core.Utilities.Concrete
{
    public class HashManager:IHashManager
    {
        public string Encrpt(string pass)
        {
            return BC.HashPassword(pass);
        }

        public bool Verify(string pass, string hashPassword)
        {
            return BC.Verify(pass, hashPassword);
        }
    }
}
