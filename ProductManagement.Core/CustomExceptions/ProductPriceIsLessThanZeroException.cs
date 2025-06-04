namespace ProductManagement.Core.CustomExceptions
{
    public sealed class ProductPriceIsLessThanZeroException : ProductManagementException
    {
        public ProductPriceIsLessThanZeroException() 
            : base("The price of product is less than 0")
        {
        }
    }
}
