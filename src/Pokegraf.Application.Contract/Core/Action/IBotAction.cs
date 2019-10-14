using System;
using MediatR;
using OperationResult;
using Pokegraf.Common.ErrorHandling;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Application.Contract.Core.Action
{
    public interface IBotAction : IRequest<Status<ResultError>>
    {
        DateTime Timestamp { get; set; }
        int MessageId { get; set; }
        Chat Chat { get; set; }
        User From { get; set; }
        string Text { get; set; }
        bool CanHandle(string condition);
    }
}