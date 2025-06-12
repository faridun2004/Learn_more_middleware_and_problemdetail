using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V2.Commands.CreateUser
{
    public class CreateUserCommandV2 : IRequest<UserV2>
    {
        private string _userName = string.Empty;
        public string Username
        {
            get => _userName;
            set
            {
                var trimmed = value?.Trim().Replace(" ", "") ?? string.Empty;
                _userName = string.IsNullOrEmpty(trimmed)
                    ? string.Empty
                    : char.ToUpper(trimmed[0]) + trimmed[1..].ToLower();
            }
        }
        public string Email { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
    }
}
