using MediatR;

namespace Pokegraf.Domain.Stats.Command.AddOneToFusionRequests
{
    public class AddOneToFusionRequestsCommand : IRequest
    {
        public int UserId { get; set; }
    }
}