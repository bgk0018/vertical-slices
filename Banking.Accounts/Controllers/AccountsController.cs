using System.Threading.Tasks;
using Banking.Accounts.Exceptions;
using Banking.Accounts.Models;
using Banking.Accounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Accounts.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly AccountsService _service;

        public AccountsController(AccountsService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] TransientAccountModel model)
        {
            var result = await _service.Create(model);
            return Created($"{result.Id}", result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] TransientAccountModel model)
        {
            var result = await _service.Replace(id, model);

            if (ControllerContext.HttpContext.Request.Headers["representation"] == "minimal")
                return NoContent();

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
