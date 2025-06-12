using MediatR;
using Microsoft.EntityFrameworkCore;
using RegisterService.Data;
using RegisterService.DTO;
using RegisterService.Entity;
using RegisterService.Exceptions;

namespace RegisterService.UseCases.Users.V1.Commands.CreateUser
{
    public class CreateUserCommandHandlerV1 : IRequestHandler<CreateUserCommandV1, UserV1>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CreateUserCommandHandlerV1> _logger;

        public CreateUserCommandHandlerV1(AppDbContext context, ILogger<CreateUserCommandHandlerV1> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<UserV1> Handle(CreateUserCommandV1 request, CancellationToken cancellationToken)
        {
            if (_context.Users.Any(u => u.Username == request.Name))
                throw new AppException("Username already exists");

            var user = new User
            {
                Username = request.Name,
                BirthDate = DateTime.UtcNow.AddYears(-request.Age), 
            };

            try
            {
                 _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Created new user with ID {UserId}", user.Id);

                return new UserV1
                {
                    Id = user.Id,
                    Name = user.Username,
                    Age = request.Age
                };
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error saving user to database");
                throw new ApplicationException("Error creating user", ex);
            }
        }
    }
}

