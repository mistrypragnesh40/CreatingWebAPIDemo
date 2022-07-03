using System.ComponentModel.DataAnnotations;

namespace CRUDOperationUsingWEBAPI.Data
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        public string LastName { get; set; }   = null!;
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [MaxLength(5)]
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
    }
}
