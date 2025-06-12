using FluentValidation;

namespace RegisterService.UseCases.Users.V2.Commands.CreateUser
{
    public class CreateUserCommandV2Validator : AbstractValidator<CreateUserCommandV2>
    {
        public CreateUserCommandV2Validator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty")
                                 .EmailAddress().WithMessage("Invalid email format");
        }
    }

}
