using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V2.Commands.CreateUser
{
    public class CreateUserCommandV2 : IRequest<UserV2>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
