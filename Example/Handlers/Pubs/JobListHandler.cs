using MediatR;
using PubsDBFirst;
using Tots.DbFilter;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;

namespace Example.Handlers.Pubs
{
    public class JobListRequest : TotsDbListRequest<Job>
    {

    }

    public class JobListHandler : IRequestHandler<JobListRequest, TotsDbListResponse<Job>>
    {
        PubsContext _context;
        public JobListHandler(PubsContext context)
        {
            _context = context;
        }

        public Task<TotsDbListResponse<Job>> Handle(JobListRequest request, CancellationToken cancellationToken)
        {
            TotsDbQuery<Job> query = new TotsDbQuery<Job>(_context, request);

            return query.Execute();
        }
    }
}
