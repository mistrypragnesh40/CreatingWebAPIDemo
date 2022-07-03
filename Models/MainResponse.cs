namespace CRUDOperationUsingWEBAPI.Models
{
    public class MainResponse
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public object? Content { get; set; }
    }
}
