using FluentValidation;

namespace RegisterService.UseCases.Users.V1.Commands.CreateUser
{
    public class CreateUserCommandV1Validator:  AbstractValidator<CreateUserCommandV1>
    { 
        public CreateUserCommandV1Validator() 
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(18)
                .WithMessage("User must be at least 18 years old");
        }
    }
}
