
namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);
    internal class GetProductsByCategoryQueryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                .Where(p => p.Category.Contains(query.Category))
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            var productDtos = products.Adapt<List<ProductDto>>();

            return new GetProductsByCategoryResult(productDtos);
        }
    }
}
