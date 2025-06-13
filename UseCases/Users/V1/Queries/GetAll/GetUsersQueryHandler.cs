using MediatR;
using Microsoft.EntityFrameworkCore;
using RegisterService.Data;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V1.Queries.GetAll
{
    public class GetUsersQueryHandler: IRequestHandler<GetUsersQueryV1, List<UserV1>>
    {
        private readonly AppDbContext _appDbContext;
        public GetUsersQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<UserV1>> Handle(GetUsersQueryV1 request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users
                .AsNoTracking()
                .Select(u => new UserV1
                {
                    Id = u.Id,
                    Name = u.Username,
                    Age = u.BirthDate.HasValue ? DateTime.Now.Year - u.BirthDate.Value.Year : 0
                }).ToListAsync(cancellationToken);
        }
    }
}
