namespace CRUDOperationUsingWEBAPI.Models
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string UserAvatar { get; set; }
    }
}
