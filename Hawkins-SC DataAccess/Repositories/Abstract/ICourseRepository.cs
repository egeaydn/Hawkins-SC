using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface ICourseRepository : IRepository<Course>
	{
		Task<Course?> GetCourseWithClassesAsync(Guid courseId);
		Task<Course?> GetCourseByCodeAsync(string code);
		Task<IEnumerable<Course>> GetAllCoursesWithDetailsAsync();
		Task<IEnumerable<Course>> SearchCoursesAsync(string searchTerm);
	}
}
