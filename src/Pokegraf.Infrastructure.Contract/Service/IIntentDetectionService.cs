using System.Threading.Tasks;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Model;

namespace Pokegraf.Infrastructure.Contract.Service
{
    public interface IIntentDetectionService
    {
        Task<Result<IntentDto>> GetIntent(DetectIntentQuery query);
    }
}