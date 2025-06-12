using MediatR;
using Microsoft.EntityFrameworkCore;
using RegisterService.Data;
using RegisterService.DTO;
using RegisterService.Entity;
using RegisterService.Exceptions;

namespace RegisterService.UseCases.Users.V2.Commands.CreateUser
{
    public class CreateUserCommandHandlerV2 : IRequestHandler<CreateUserCommandV2, UserV2>
    {
        private readonly AppDbContext _context;

        public CreateUserCommandHandlerV2(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<UserV2> Handle(CreateUserCommandV2 request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new AppException("Email already exists");
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,          
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new UserV2
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }
}
