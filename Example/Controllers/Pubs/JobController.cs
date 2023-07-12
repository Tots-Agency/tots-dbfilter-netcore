using Example.Handlers.Pubs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers.Pubs
{
    public class JobController : APIBaseController
    {
        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] JobListRequest request) => Ok(await Mediator.Send(request));
    }
}
