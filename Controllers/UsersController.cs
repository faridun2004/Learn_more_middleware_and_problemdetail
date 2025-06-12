using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegisterService.DTO;
using RegisterService.UseCases.Users.V1.Commands.CreateUser;
using RegisterService.UseCases.Users.V1.Queries.GetById;
using RegisterService.UseCases.Users.V2.Commands.CreateUser;
using RegisterService.UseCases.Users.V2.Queries.GetById;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ========== V1 ACTIONS ==========
        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserV1>> GetUserV1(int id)
        {
            var query = new GetUserByIdQueryV1 { Id = id };
            var result = await _mediator.Send(query);

            if (result == null) return NotFound();
            return Ok(result);
        }
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<ActionResult<UserV1>> CreateUserV1(CreateUserCommandV1 command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserV1), new { id = result.Id, version = "1" }, result);
        }

        // ========== V2 ACTIONS ==========

        [MapToApiVersion("2.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserV2>> GetUserV2(int id)
        {
            var query = new GetUserByIdQueryV2 { Id = id };
            var result = await _mediator.Send(query);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<ActionResult<UserV2>> CreateUserV2(CreateUserCommandV2 command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserV2), new { id = result.Id, version = "2" }, result);
        }
    }
}

