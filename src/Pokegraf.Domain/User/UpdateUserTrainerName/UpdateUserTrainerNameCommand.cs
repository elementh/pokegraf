using Pokegraf.Common.Request;
using Pokegraf.Common.Result;

namespace Pokegraf.Domain.User.UpdateUserTrainerName
{
    public class UpdateUserTrainerNameCommand : Request<Result>
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