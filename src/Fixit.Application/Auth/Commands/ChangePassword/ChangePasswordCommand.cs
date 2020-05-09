using Fixit.Shared.CQRS;

namespace Fixit.Application.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : ICommand
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}