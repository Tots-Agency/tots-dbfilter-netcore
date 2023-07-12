using MediatR;
using PubsDBFirst;
using Tots.DbFilter;
using Tots.DbFilter.Requests;
using Tots.DbFilter.Responses;

namespace Example.Handlers.Pubs
{
    public class TitleListRequest : TotsDbListRequest<Title>
    {

    }

    public class TitleListHandler : IRequestHandler<TitleListRequest, TotsDbListResponse<Title>>
    {
        PubsContext _context;
        public TitleListHandler(PubsContext context)
        {
            _context = context;
        }

        public Task<TotsDbListResponse<Title>> Handle(TitleListRequest request, CancellationToken cancellationToken)
        {
            TotsDbQuery<Title> query = new TotsDbQuery<Title>(_context, request);

            return query.Execute();
        }
    }
}
