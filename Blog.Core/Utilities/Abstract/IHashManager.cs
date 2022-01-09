using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities.Abstract
{
    public interface IHashManager
    {
        public string Encrpt(string pass);
        public bool Verify(string pass, string hashPassword);
    }
}
