using MediatR;
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
            var user = await _context.Users.FindAsync( request.Id, cancellationToken);

            if (user == null)
                throw new NotFoundException($"User with ID {request.Id} not found");

            return new UserV2
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
