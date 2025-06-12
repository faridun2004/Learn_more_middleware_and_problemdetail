using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V1.Queries.GetAll
{
    public record GetUsersQueryV1(): IRequest<List<UserV1>>;
}
