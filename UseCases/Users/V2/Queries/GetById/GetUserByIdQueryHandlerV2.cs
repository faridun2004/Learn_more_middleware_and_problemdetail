using MediatR;
using Microsoft.EntityFrameworkCore;
using RegisterService.Data;
using RegisterService.DTO;
using RegisterService.Exceptions;

namespace RegisterService.UseCases.Users.V2.Queries.GetById
{
    public class GetUserByIdQueryHandlerV2 : IRequestHandler<GetUserByIdQueryV2, UserV2>
    {
        private readonly AppDbContext _context;

        public GetUserByIdQueryHandlerV2(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserV2> Handle(GetUserByIdQueryV2 request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.Id)
                .Select(u => new UserV2
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                throw new NotFoundException($"User with ID {request.Id} not found");

            return user;
        }

    }
}
