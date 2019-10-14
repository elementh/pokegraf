using System;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.User.Query.FindUser
{
    public class FindUserQuery : IRequest<Result<Entity.User, ResultError>>
    {
        public DateTime Timestamp { get; }

        public FindUserQuery()
        {
            Timestamp = DateTime.UtcNow;
        }
        
        public int UserId { get; set; }
    }
}