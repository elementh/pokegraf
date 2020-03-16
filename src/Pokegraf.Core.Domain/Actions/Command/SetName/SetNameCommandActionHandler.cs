using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Navigator;
using Navigator.Abstraction;
using Navigator.Actions;
using Navigator.Extensions.Store;
using Pokegraf.Common.Resources;
using Pokegraf.Core.Entity;
using Pokegraf.Persistence.Context;
using Telegram.Bot.Types.Enums;

namespace Pokegraf.Core.Domain.Actions.Command.SetName
{
    public class SetNameCommandActionHandler : ActionHandler<SetNameCommandAction>
    {
        protected readonly ILogger<SetNameCommandActionHandler> Logger;
        protected readonly PokegrafDbContext DbContext;

        public SetNameCommandActionHandler(INavigatorContext ctx, PokegrafDbContext dbContext) : base(ctx)
        {
            DbContext = dbContext;
        }

        public override async Task<Unit> Handle(SetNameCommandAction request, CancellationToken cancellationToken)
        {
            var commandArgs = Ctx.Update.Message.Text?.Split(" ");

            var newName = commandArgs != null && commandArgs.Length > 1
                ? commandArgs[1]
                : string.Empty;

            if (newName.Length <= 25)
            {
                try
                {
                    Ctx.GetUser<Trainer>().TrainerName = newName;

                    await DbContext.SaveChangesAsync(cancellationToken);

                    await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(),
                        $"Alright *{Ctx.GetUser<Trainer>().TrainerName}*, your new name has been set!",
                        ParseMode.Markdown,
                        cancellationToken: cancellationToken);

                    return default;
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Unknown error setting a new trainer name for trainer {TrainerId}", Ctx.GetTelegramUser().Id);
                }
            }

            await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(),
                PokegrafResources.SetNameErrorMessage,
                ParseMode.Markdown,
                cancellationToken: cancellationToken);

            return default;
        }
    }
}