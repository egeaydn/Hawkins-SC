using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Abstract;

namespace Hawkins_SC.Concrate
{
	public class Course : BaseEntity<Guid>
	{
		public string Title { get; set; }
		public string CourseCode { get; set; }
		public string? Description { get; set; }
		public int Credits { get; set; }

		// Navigation Properties
		public virtual ICollection<Class> Classes { get; set; }
	}
}
