using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
