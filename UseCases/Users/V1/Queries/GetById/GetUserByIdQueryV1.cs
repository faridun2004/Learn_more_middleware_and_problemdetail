using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V1.Queries.GetById
{
    public class GetUserByIdQueryV1 : IRequest<UserV1>
    {
        public int Id { get; set; }
    }
}
