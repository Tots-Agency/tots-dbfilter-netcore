using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;

namespace Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public class APIBaseController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
