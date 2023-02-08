using System.ComponentModel.DataAnnotations;

namespace Dentistry.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } =string.Empty;
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
}
