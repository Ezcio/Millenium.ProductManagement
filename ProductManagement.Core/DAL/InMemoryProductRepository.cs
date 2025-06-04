using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.DomainModels;

namespace ProductManagement.Core.DAL;

public sealed class InMemoryProductRepository : IProductRepository
{
    private List<Product> products = new(); //It should be a collection that supports asynchronicity
    private int nextId = 0;

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Task.FromResult(products);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(products.SingleOrDefault(p => p.Id == id));
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        product.SetId(nextId++);
        products.Add(product);
        return await Task.FromResult(product);
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var existing = products.SingleOrDefault(p => p.Id == product.Id);
        if (existing == null)
        {
            throw new ProductNotFoundException(product.Id);
        }

        products.Remove(existing);
        products.Add(product);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        products.Remove(product);
        return await Task.FromResult(true);
    }
}

