using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Features.Accounts
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetCollection.Request()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(Get.Request request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(Create.Request request)
        {
            var result = await _mediator.Send(request);
            return Created($"{result.Id}", result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(Replace.Request request)
        {
            var result = await _mediator.Send(request);

            if (ControllerContext.HttpContext.Request.Headers["representation"] == "minimal")
                return NoContent();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(Delete.Request request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
