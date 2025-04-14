using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Interfaces.IServices
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string passwordHash, string inputPassword);

    }
}