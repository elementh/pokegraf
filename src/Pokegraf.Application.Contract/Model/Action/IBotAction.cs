using MediatR;
using Pokegraf.Common.Result;
using Telegram.Bot.Types;

namespace Pokegraf.Application.Contract.Model.Action
{
    public interface IBotAction : IRequest<Result>
    {
        int MessageId { get; set; }
        Chat Chat { get; set; }
        User From { get; set; }
        string Text { get; set; }
        bool CanHandle(string condition);
    }
}