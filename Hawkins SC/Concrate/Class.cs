using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Abstract;

namespace Hawkins_SC.Concrate
{
	public class Class : BaseEntity<Guid>
	{
		public Guid CourseId { get; set; }         // Burası Kurs id sinin foreign keyi -> Course
		public Guid? TeacherId { get; set; }       // Burası Öğretmenler id sinin foreign keyi -> Teacher (nullable, henüz atanmamış olabilir)
		public int Year { get; set; }              // 2024, 2025 örneğin
		public int Capacity { get; set; } = 30;    // Max öğrenci sayısı

		// Navigation Properties
		public virtual Course Course { get; set; }
		public virtual Teacher? Teacher { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
}