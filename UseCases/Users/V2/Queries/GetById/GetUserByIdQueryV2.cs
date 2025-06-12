using MediatR;
using RegisterService.DTO;

namespace RegisterService.UseCases.Users.V2.Queries.GetById
{
    public class GetUserByIdQueryV2 : IRequest<UserV2>
    {
        public int Id { get; set; }
    }
}
