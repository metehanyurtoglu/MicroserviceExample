using Catalog.API.Models;
using Core.Application.Abstractions.CQRS;
using Marten.Pagination;

namespace Catalog.API.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
