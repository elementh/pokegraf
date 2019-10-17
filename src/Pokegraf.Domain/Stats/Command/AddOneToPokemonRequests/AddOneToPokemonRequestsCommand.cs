using MediatR;

namespace Pokegraf.Domain.Stats.Command.AddOneToPokemonRequests
{
    public class AddOneToPokemonRequestsCommand : IRequest
    {
        public int UserId { get; set; }
    }
}