using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.Products.Models;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
{
    public async Task<PagedResult<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var filter = mapper.Map<ProductFilter>(request);
        var result = await productRepository.GetPagedAsync(filter, cancellationToken);
        var products = mapper.Map<List<ProductResponse>>(result.Items);

        return new PagedResult<ProductResponse>(products, result.PageNumber, result.PageSize, result.TotalCount);
    }
}
