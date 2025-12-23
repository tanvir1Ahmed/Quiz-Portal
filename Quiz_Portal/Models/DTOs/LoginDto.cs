using System.ComponentModel.DataAnnotations;

namespace Quiz_Portal.Models.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
