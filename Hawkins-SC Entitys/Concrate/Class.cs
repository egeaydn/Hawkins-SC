using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Abstract;
using Hawkins_SC.Enums;

namespace Hawkins_SC.Concrate
{
	public class Class : BaseEntity<Guid>
	{
		public Guid CourseId { get; set; }           // FK -> Course
		public Guid? TeacherId { get; set; }         // FK -> Teacher (nullable - henüz atanmamış olabilir)

		public Semester Semester { get; set; }       // Fall, Spring, Summer (Enum)
		public int Year { get; set; }                // 2024, 2025
		public int Capacity { get; set; } = 30;      // Max öğrenci sayısı

		// Navigation Properties
		public virtual Course Course { get; set; } = null!;
		public virtual Teacher? Teacher { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
	}
}