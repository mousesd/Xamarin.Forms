using System.Threading.Tasks;
using Firebase.Auth;

using FormsApp21.Services;

namespace FormsApp21.iOS.Services
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> Login(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInAsync(email, password);
            return await user.GetIdTokenAsync();
        }
    }
}
