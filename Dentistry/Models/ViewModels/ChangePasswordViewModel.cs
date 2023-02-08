namespace Dentistry.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
