using System.Collections.Generic;
using Pokegraf.Domain.User.Command.UpdateUserTrainerName;

namespace Pokegraf.Application.Implementation.Core.Actions.Callback.Start
{
    public static class StartCallbackActionExtension
    {
        public static UpdateUserTrainerNameCommand MapToUpdateUserTrainerNameCommand(this StartCallbackAction request)
        {
            return new UpdateUserTrainerNameCommand
            {
                UserId = request.From.Id,
                TrainerName = request.Data.GetValueOrDefault("requested_name")
            };
        }
    }
}