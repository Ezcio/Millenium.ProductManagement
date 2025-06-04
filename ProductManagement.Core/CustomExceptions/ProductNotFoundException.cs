namespace ProductManagement.Core.CustomExceptions
{
    public sealed class ProductNotFoundException : ProductManagementException
    {
        public ProductNotFoundException(int id) 
            : base($"Product with id {id} was not found")
        {
        }
    }
}
