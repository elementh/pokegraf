using System.Threading.Tasks;
using Pokegraf.Infrastructure.Contract.Dto;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IIntentDetectionService
    {
        Task<IntentDto> GetIntent(string userQuery);
    }
}