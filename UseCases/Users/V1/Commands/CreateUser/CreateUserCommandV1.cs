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
            set=>_name=value.Trim().Replace(" ",""); 
        }
        public int Age { get; set; }
    }
}
