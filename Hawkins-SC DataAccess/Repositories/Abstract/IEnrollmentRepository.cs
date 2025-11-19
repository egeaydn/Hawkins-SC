using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;

namespace Hawkins_SC_DataAccess.Repositories.Abstract
{
	public interface IEnrollmentRepository : IRepository<Enrollment>
	{
		Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(Guid studentId);
		Task<IEnumerable<Enrollment>> GetEnrollmentsByClassAsync(Guid classId);
		Task<Enrollment?> GetEnrollmentWithGradesAsync(Guid enrollmentId);
		Task<bool> CheckEnrollmentExistsAsync(Guid studentId, Guid classId);
		Task<IEnumerable<Enrollment>> GetActiveEnrollmentsAsync(Guid studentId);
	}
}
