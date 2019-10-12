using System;
using System.Collections.Generic;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.Core.User.Query.FindAllUsers
{
    public class FindAllUsersQuery : IRequest<Result<IEnumerable<Entity.User>, ResultError>>
    {
        public DateTime Timestamp { get; }
        
        protected FindAllUsersQuery()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}