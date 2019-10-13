﻿using System.Collections.Generic;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;

namespace Pokegraf.Domain.Core.Chat.Query.FindAllPrivateChats
{
    public class FindAllPrivateChatsQuery : IRequest<Result<IEnumerable<Entity.Chat>, ResultError>>
    {
        
    }
}