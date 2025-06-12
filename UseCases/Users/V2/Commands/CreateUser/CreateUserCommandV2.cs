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
                var trimmed = value?.Trim().Replace(" ","") ?? string.Empty;
                if (trimmed.Length > 0)
                    _userName = char.ToUpper(trimmed[0]) + trimmed.Substring(1).ToLower();
                else
                    _userName = string.Empty;
            }
        }
        public string Email { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
    }
}
