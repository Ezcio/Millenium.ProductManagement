using ProductManagement.Core.CustomExceptions;
using ProductManagement.Core.Enums;

namespace ProductManagement.Core.DomainModels;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public ProductType ProductType { get; private set; }

    public Product(string name, decimal price, ProductType productType)
    {
        Create(name, price, productType);
    }

    public Product(int id, string name, decimal price, ProductType productType)
    {
        Create(name, price, productType);
    }

    public void Create(string name, decimal price, ProductType productType)
    {
        if (price < 0M)
        {
            throw new ProductPriceIsLessThanZeroException();
        }

        Name = name;
        Price = price;
        ProductType = productType;
    }

    public void Create(int id, string name, decimal price, ProductType productType)
    {
        if (price < 0M)
        {
            throw new ProductPriceIsLessThanZeroException();
        }

        Id = id;
        Name = name;
        Price = price;
        ProductType = productType;
    }

    public void SetId(int id)
    {
        Id = id;
    }
}
