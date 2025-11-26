using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC_DataAccess.Repositories.Abstract;

namespace Hawkins_SC_DataAccess.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		// Repository arayüzleri (projende bunlar varsa; yoksa aynı ada sahip arayüzleri oluştur)
		IStudentRepository Students { get; }
		ITeacherRepository Teachers { get; }
		ICourseRepository Courses { get; }
		IClassRepository Classes { get; }
		IEnrollmentRepository Enrollments { get; }
		IGradeRepository Grades { get; }

		// Save / Transaction yönetimi
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		int SaveChanges(); // isteğe bağlı, sen tercih ettik

		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
	}
}
