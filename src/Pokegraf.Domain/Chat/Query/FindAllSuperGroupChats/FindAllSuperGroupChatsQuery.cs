﻿using System.Collections.Generic;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.Chat.Query.FindAllSuperGroupChats
{
    public class FindAllSuperGroupChatsQuery : IRequest<Result<IEnumerable<Entity.Chat>, ResultError>>
    {
        
    }
}