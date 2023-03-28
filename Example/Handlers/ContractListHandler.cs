using Example.Entities;
using Example.Persistence;
using MediatR;
using Tots.DbFilter;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;

namespace Example.Handlers
{
    public class ContractListRequest : TotsDbListRequest<Contract>
    {

    }

    public class ContractListHandler : IRequestHandler<ContractListRequest, TotsDbListResponse<Contract>>
    {
        GreenwayContext _context;

        public ContractListHandler(GreenwayContext context)
        {
            _context = context;
        }

        public async Task<TotsDbListResponse<Contract>> Handle(ContractListRequest request, CancellationToken cancellationToken)
        {
            TotsDbQuery<Contract> dbFilter = new TotsDbQuery<Contract>(_context, request);
            //dbFilter.GetQueryRequest().AddWhere();
            return await dbFilter.Execute();
        }
    }
}
