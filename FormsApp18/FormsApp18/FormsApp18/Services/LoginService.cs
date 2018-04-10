using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp18.Services
{
    public class LoginService : ILogin
    {
        private readonly Dictionary<string, string> userCredentials;

        public LoginService()
        {
            userCredentials = new Dictionary<string, string>
            {
                { "us@sad.com", "aaaaaaaa" },
                { "user2@sad.com", "Userabc123" },
                { "user3@sad.com", "!A@3534" },
                { "mousesd@gmail.com", "1234" }
            };
        }

        public Task<bool> Login(string email, string password)
        {
            return userCredentials.ContainsKey(email) 
                ? Task.FromResult(userCredentials[email] == password) 
                : Task.FromResult(false);
        }
    }
}
