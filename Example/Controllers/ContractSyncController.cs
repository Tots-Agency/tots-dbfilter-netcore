using Example.Entities;
using Example.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tots.DbFilter.Requests;

namespace Example.Controllers
{
    public class ContractSyncController: APIBaseController
    {
        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] ContractListRequest request) => Ok(await Mediator.Send(request));

        [HttpGet("items")]
        public async Task<IActionResult> List([FromQuery] TotsDbListRequest<ProjectAcknolegmentItem> request)
        {
            return Ok(await Mediator.Send(new GetProjectAcknolegmentItemsRequest { Request = request }));
        }
    }
}
