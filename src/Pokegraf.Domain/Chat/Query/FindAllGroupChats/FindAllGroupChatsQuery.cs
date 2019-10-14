﻿using System.Collections.Generic;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.Chat.Query.FindAllGroupChats
{
    public class FindAllGroupChatsQuery : IRequest<Result<IEnumerable<Entity.Chat>, ResultError>>
    {
        
    }
}