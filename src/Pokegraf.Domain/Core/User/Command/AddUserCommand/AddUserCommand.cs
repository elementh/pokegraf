using System;
using MediatR;

namespace Pokegraf.Domain.Core.User.Command.AddUserCommand
{
    public class AddUserCommand : IRequest
    {
        public DateTime Timestamp { get; set; }

        public AddUserCommand()
        {
            Timestamp = DateTime.UtcNow;
        }
        
        public int UserId { get; set; }
        public bool UserIsBot { get; set; }
        public string UserLanguageCode { get; set; }
        public string UserUsername { get; set; }
    }
}