using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, MinLength(10, ErrorMessage = "Password does not have the minimal length (10)")]
        public string Password { get; set; }
    }
}
