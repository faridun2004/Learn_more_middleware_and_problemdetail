using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V1.Queries.GetById
{
    public record GetUserByIdQueryV1(int Id) : IRequest<UserV1>;
}
