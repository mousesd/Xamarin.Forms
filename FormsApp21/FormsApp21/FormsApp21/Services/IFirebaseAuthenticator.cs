using System.Threading.Tasks;

namespace FormsApp21.Services
{
    public interface IFirebaseAuthenticator
    {
        Task<string> Login(string email, string password);
    }
}
