using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V2.Queries.GetById
{
    public record GetUserByIdQueryV2(int Id) : IRequest<UserV2>;
}
