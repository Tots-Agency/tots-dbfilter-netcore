using Example.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers
{
    public class ContractSyncController: APIBaseController
    {
        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] ContractListRequest request) => Ok(await Mediator.Send(request));
    }
}
