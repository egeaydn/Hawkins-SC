using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Hawkins_SC.Enums;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface IGradeRepository : IRepository<Grade>
	{
		Task<IEnumerable<Grade>> GetGradesByEnrollmentAsync(Guid enrollmentId);
		Task<IEnumerable<Grade>> GetGradesByStudentAsync(Guid studentId);
		Task<IEnumerable<Grade>> GetGradesByClassAsync(Guid classId);
		Task<Grade?> GetGradeByTypeAsync(Guid enrollmentId, GradeType gradeType);
		Task<decimal> CalculateEnrollmentAverageAsync(Guid enrollmentId);
		Task<decimal> CalculateGPAAsync(Guid studentId);
	}
}
