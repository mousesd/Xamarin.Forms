using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using FormsApp21.Services;
using FormsApp21.iOS.Services;

[assembly:Dependency(typeof(FirebaseAuthenticator))]
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
