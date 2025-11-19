using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface IStudentRepository : IRepository<Student>
	{
		Task<Student?> GetStudentWithEnrollmentsAsync(Guid studentId);
		Task<Student?> GetStudentWithGradesAsync(Guid studentId);
		Task<IEnumerable<Student>> GetActiveStudentsAsync();
		Task<IEnumerable<Student>> SearchStudentsByNameAsync(string searchTerm);
	}
}
