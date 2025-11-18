using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC_Bussines.Common;
using Hawkins_SC_Bussines.DTOs;

namespace Hawkins_SC_Bussines.Contracts
{
	public interface IStudentService
	{
		Task<ServiceResult<StudentDto>> GetByIdAsync(Guid id);
		Task<ServiceResult<IEnumerable<StudentDto>>> GetAllAsync();
		Task<ServiceResult<StudentDto>> CreateAsync(CreateStudentDto dto);
		Task<ServiceResult<StudentDto>> UpdateAsync(Guid id, CreateStudentDto dto);
		Task<ServiceResult<bool>> DeleteAsync(Guid id);
	}
}
