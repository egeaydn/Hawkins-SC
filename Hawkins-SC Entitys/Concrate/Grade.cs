using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Abstract;
using Hawkins_SC.Enums;

namespace Hawkins_SC.Concrate
{
	public class Grade : BaseEntity<Guid>
	{
		public Guid EnrollmentId { get; set; }     // FK -> Enrollment
		public decimal GradeValue { get; set; }    // 0-100 arası not
		public GradeType GradeType { get; set; }   // Midterm, Final, Quiz
		public DateTime DateGiven { get; set; } = DateTime.UtcNow;
		public Guid? GivenByTeacherId { get; set; } // FK -> Teacher (kim verdi)

		// Navigation Properties
		public virtual Enrollment Enrollment { get; set; }
		public virtual Teacher? GivenByTeacher { get; set; }
	}
}
