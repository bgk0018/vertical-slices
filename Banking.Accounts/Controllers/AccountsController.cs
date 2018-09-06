using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Core;
using Banking.Accounts.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [Route("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.Get());
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.Get(id));
        }

        [Route("")]
        public async Task<IActionResult> Post([FromBody] TransientAccountModel model)
        {
            var result = await _service.Create(model);
            return Created($"{result.Id}", result);
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] TransientAccountModel model)
        {
            var result = await _service.Replace(id, model);

            if (ControllerContext.HttpContext.Request.Headers["representation"] == "minimal")
                return NoContent();

            return Ok(result);
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument document)
        {
            var result = await _service.Update(id, document);

            if (ControllerContext.HttpContext.Request.Headers["representation"] == "minimal")
                return NoContent();

            return Ok(result);
        }
    }
}
