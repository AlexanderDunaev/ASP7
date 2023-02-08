using Microsoft.AspNetCore.Identity;

namespace Dentistry.Models
{
	public class User : IdentityUser
	{
		public string? Name { get; set; }
	}
}
