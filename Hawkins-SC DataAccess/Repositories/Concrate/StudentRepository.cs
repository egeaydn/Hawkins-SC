using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hawkins.DataAccess.Context;
using Hawkins_SC.Concrate;
using Hawkins_SC_DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Hawkins_SC_DataAccess.Repositories.Concrete
{
	public class StudentRepository : Repository<Student>, IStudentRepository
	{
		public StudentRepository(HawkinsDbContext context) : base(context) { }

		public async Task<Student?> GetStudentWithEnrollmentsAsync(Guid studentId)
		{
			return await _context.Students
				.Include(s => s.Enrollments)
					.ThenInclude(e => e.Class)
				.FirstOrDefaultAsync(s => s.Id == studentId);
		}

		public async Task<Student?> GetStudentWithGradesAsync(Guid studentId)
		{
			return await _context.Students
				.Include(s => s.Enrollments)
					.ThenInclude(e => e.Grades)
				.FirstOrDefaultAsync(s => s.Id == studentId);
		}

		public async Task<IEnumerable<Student>> GetActiveStudentsAsync()
		{
			return await _context.Students.Where(s => s.IsActive && !s.IsDeleted).ToListAsync();
		}

		public async Task<IEnumerable<Student>> SearchStudentsByNameAsync(string searchTerm)
		{
			return await _context.Students
				.Where(s => (s.FirstName + " " + s.LastName).Contains(searchTerm))
				.ToListAsync();
		}

		public Task<IEnumerable<Student>> GetPagedAsync(int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task AddRangeAsync(IEnumerable<Student> entities)
		{
			throw new NotImplementedException();
		}

		public void UpdateRange(IEnumerable<Student> entities)
		{
			throw new NotImplementedException();
		}

		public void DeleteRange(IEnumerable<Student> entities)
		{
			throw new NotImplementedException();
		}
	}
}