using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Hawkins_SC.Abstract;

namespace Hawkins_SC.Concrate
{
	internal class Teacher : BaseEntity<Guid>, IAuditableEntity
	{
		public string IdentityUserId { get; set; }  // burası Foreign Key olacak -> AspNetUsers
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime HireDate { get; set; }
		public bool IsActive { get; set; } = true;

		// Navigation Properties
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<Class> Classes { get; set; }
		public virtual ICollection<Grade> GivenGrades { get; set; }

		// Audit
		public string? CreatedBy { get; set; }
		public string? ModifiedBy { get; set; }
		public string? DeletedBy { get; set; }
	}
}
