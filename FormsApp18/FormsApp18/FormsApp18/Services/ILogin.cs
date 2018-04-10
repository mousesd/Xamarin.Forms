using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp18.Services
{
    public interface ILogin
    {
        Task<bool> Login(string email, string password);
    }
}
