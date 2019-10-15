using Pokegraf.Domain.User.Command.UpdateUserTrainerName;

namespace Pokegraf.Application.Implementation.Core.Actions.Commands.SetTrainerName
{
    public static class SetTrainerNameCommandActionExtension
    {
        public static UpdateUserTrainerNameCommand MapToUpdateUserTrainerNameCommand(this SetTrainerNameCommandAction request, string name)
        {
            return new UpdateUserTrainerNameCommand
            {
                UserId = request.From.Id,
                TrainerName = name
            };
        }
    }
}