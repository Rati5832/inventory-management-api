using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Product with id '{request.Id}' was not found.");

        return mapper.Map<ProductResponse>(product);
    }
}
