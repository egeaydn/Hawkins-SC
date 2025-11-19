using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface ITeacherRepository : IRepository<Teacher>
	{
		Task<Teacher?> GetTeacherWithClassesAsync(Guid teacherId);
		Task<Teacher?> GetTeacherWithGradesAsync(Guid teacherId);
		Task<IEnumerable<Teacher>> GetActiveTeachersAsync();
		Task<IEnumerable<Teacher>> SearchTeachersByNameAsync(string searchTerm);
	}
}
