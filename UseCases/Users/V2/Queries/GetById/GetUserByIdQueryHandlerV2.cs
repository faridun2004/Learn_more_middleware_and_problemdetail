using MediatR;
using RegisterService.Data;
using RegisterService.DTO;

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
            var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

            if (user == null) return null;

            return new UserV2
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
