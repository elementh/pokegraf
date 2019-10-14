using System;
using MediatR;
using OperationResult;
using Pokegraf.Domain.Entity;

namespace Pokegraf.Application.Contract.Action
{
    public interface IBotAction : IRequest<Status>
    {
        DateTime Timestamp { get; set; }
        int MessageId { get; set; }
        Chat Chat { get; set; }
        User From { get; set; }
        string Text { get; set; }
        bool CanHandle(string condition);
    }
}