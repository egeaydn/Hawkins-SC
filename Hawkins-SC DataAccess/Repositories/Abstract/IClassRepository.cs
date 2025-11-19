using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Hawkins_SC.Enums;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface IClassRepository : IRepository<Class>
	{
		Task<Class?> GetClassWithEnrollmentsAsync(Guid classId);
		Task<Class?> GetClassWithCourseAndTeacherAsync(Guid classId);
		Task<IEnumerable<Class>> GetClassesBySemesterAsync(Semester semester, int year);
		Task<IEnumerable<Class>> GetClassesByTeacherAsync(Guid teacherId);
		Task<int> GetCurrentEnrollmentCountAsync(Guid classId);
		Task<bool> IsClassFullAsync(Guid classId);
	}
}
