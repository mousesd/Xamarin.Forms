using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using FormsApp21.Services;
using FormsApp21.Droid.Services;

[assembly: Dependency(typeof(FirebaseAuthenticator))]
namespace FormsApp21.Droid.Services
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> Login(string email, string password)
        {
            var result = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = await result.User.GetIdTokenAsync(false);
            return token.Token;
        }
    }
}
