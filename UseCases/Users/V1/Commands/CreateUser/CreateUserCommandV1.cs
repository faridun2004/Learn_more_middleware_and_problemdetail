using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V1.Commands.CreateUser
{
    public class CreateUserCommandV1 : IRequest<UserV1>
    {
        private string _name= string.Empty;
        public string Name 
        { 
            get=>_name;
            set
            {
                var trimmed = value?.Trim().Replace(" ", "") ?? string.Empty;
                _name = string.IsNullOrEmpty(trimmed)
                    ? string.Empty
                    : char.ToUpper(trimmed[0]) + trimmed[1..].ToLower();
            }
        }
        public int Age { get; set; }
    }
}
