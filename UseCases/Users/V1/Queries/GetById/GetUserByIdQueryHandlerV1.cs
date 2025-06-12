using MediatR;
using Microsoft.EntityFrameworkCore;
using RegisterService.Data;
using RegisterService.DTO;
using RegisterService.Exceptions;

namespace RegisterService.UseCases.Users.V1.Queries.GetById
{
    public class GetUserByIdQueryHandlerV1 : IRequestHandler<GetUserByIdQueryV1, UserV1>
    {
        private readonly AppDbContext _context;

        public GetUserByIdQueryHandlerV1(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserV1> Handle(GetUserByIdQueryV1 request, CancellationToken cancellationToken)
        {        
            var user = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == request.Id)
                .Select(u => new UserV1
                {
                    Id = u.Id,
                    Name = u.Username,
                    Age = u.BirthDate.HasValue ? DateTime.Now.Year - u.BirthDate.Value.Year : 0
                }).FirstOrDefaultAsync(cancellationToken);

            return user ?? throw new NotFoundException("User not found");
        }
    }
}
