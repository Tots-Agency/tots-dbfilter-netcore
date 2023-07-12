using Example.Handlers.Pubs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers.Pubs
{
    public class SaleController : APIBaseController
    {
        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] SaleListRequest request) => Ok(await Mediator.Send(request));
    }
}
