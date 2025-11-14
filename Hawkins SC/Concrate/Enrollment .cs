using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Abstract;

namespace Hawkins_SC.Concrate
{
	public class Enrollment : BaseEntity<Guid>
	{
		public Guid ClassId { get; set; } // Burası Sınıf id sinin foreign keyi -> Class
		public Guid StudentId { get; set; } // Burası Öğrenci id sinin foreign keyi -> Öğrenci4
		public DateTime EnrollDate { get; set; } = DateTime.UtcNow;
		public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Enrolled;

		public virtual Class Class { get; set; }
		public virtual Student Student { get; set; }
		public ICollection<Grade> Grades { get; set; }

	}
}
