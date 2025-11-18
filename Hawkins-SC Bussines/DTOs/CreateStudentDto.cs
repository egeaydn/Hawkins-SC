using System;
using System.Collections.Generic;
using System.Text;

namespace Hawkins_SC_Bussines.DTOs
{
	public class CreateStudentDto
	{
		public string IdentityUserId { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
	}
}
