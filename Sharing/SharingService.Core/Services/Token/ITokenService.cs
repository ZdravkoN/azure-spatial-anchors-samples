using System.Threading.Tasks;

namespace SharingService.Core.Services.Token
{
    public interface ITokenService
    {
        Task<string> RequestToken();
    }
}
