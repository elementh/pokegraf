using MediatR;

namespace Pokegraf.Domain.Core.User.Command.UpdateUserTrainerName
{
    public class UpdateUserTrainerNameCommand : IRequest
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Trainer name of the user.
        /// </summary>
        public string TrainerName { get; set; }
    }
}