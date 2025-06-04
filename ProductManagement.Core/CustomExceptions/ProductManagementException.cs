namespace ProductManagement.Core.CustomExceptions
{
    public class ProductManagementException : Exception
    {
        public ProductManagementException(string message)
            : base(message)
        {
            
        }
    }
}
