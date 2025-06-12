using MediatR;
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
            var user = await _context.Users.FindAsync(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundException($"User with ID {request.Id} not found");

            return new UserV1
            {
                Id = user.Id,
                Name = user.Username,
                Age = user.BirthDate.HasValue ? DateTime.Now.Year - user.BirthDate.Value.Year : 0
            };
        }
    }
}
