using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Hawkins_SC.Concrate
{
	public class ApplicationUser : IdentityUser
	{
		// Identity'den gelen Id, UserName, Email, PasswordHash vb. zaten var

		// Ek custom alanlar
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		// JWT için (ileride kullanılacak)
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }

		// Navigation Properties
		// Not: Student veya Teacher one-to-one relationship
		// Bu navigation'lar Student/Teacher entity'lerinde tanımlı
	}
}
