using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Hawkins_SC_Bussines.Common;
using Hawkins_SC_Bussines.Contracts;
using Hawkins_SC_Bussines.DTOs;

namespace Hawkins_SC_Bussines.Services
{
	public class StudentService : IStudentService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateStudentDto> _createValidator;

		public StudentService(IUnitOfWork uow, IMapper mapper, IValidator<CreateStudentDto> createValidator)
		{
			_uow = uow;
			_mapper = mapper;
			_createValidator = createValidator;
		}

		public async Task<ServiceResult<StudentDto>> GetByIdAsync(Guid id)
		{
			var student = await _uow.Students.GetByIdAsync(id);
			if (student == null) return ServiceResult<StudentDto>.Fail("Student not found");

			var dto = _mapper.Map<StudentDto>(student);
			return ServiceResult<StudentDto>.Ok(dto);
		}

		public async Task<ServiceResult<IEnumerable<StudentDto>>> GetAllAsync()
		{
			var list = await _uow.Students.GetAllAsync();
			var dtos = _mapper.Map<IEnumerable<StudentDto>>(list);
			return ServiceResult<IEnumerable<StudentDto>>.Ok(dtos);
		}

		public async Task<ServiceResult<StudentDto>> CreateAsync(CreateStudentDto dto)
		{
			var validation = await _createValidator.ValidateAsync(dto);
			if (!validation.IsValid)
			{
				var errors = new Dictionary<string, string>();
				foreach (var err in validation.Errors) errors[err.PropertyName] = err.ErrorMessage;
				return ServiceResult<StudentDto>.Fail("ValidationFailed", errors);
			}

			// Business rule: prevent duplicate by IdentityUserId or Email
			var existsByUser = await _uow.Students.AnyAsync(s => s.IdentityUserId == dto.IdentityUserId);
			if (existsByUser) return ServiceResult<StudentDto>.Fail("Student with given IdentityUserId already exists");

			var existsByEmail = await _uow.Students.AnyAsync(s => s.Email == dto.Email);
			if (existsByEmail) return ServiceResult<StudentDto>.Fail("Student with given email already exists");

			var entity = _mapper.Map<Student>(dto);
			await _uow.Students.AddAsync(entity);
			await _uow.SaveChangesAsync();

			var resultDto = _mapper.Map<StudentDto>(entity);
			return ServiceResult<StudentDto>.Ok(resultDto);
		}

		public async Task<ServiceResult<StudentDto>> UpdateAsync(Guid id, CreateStudentDto dto)
		{
			var student = await _uow.Students.GetByIdAsync(id);
			if (student == null) return ServiceResult<StudentDto>.Fail("Student not found");

			// Map update fields
			_mapper.Map(dto, student);
			_uow.Students.Update(student);
			await _uow.SaveChangesAsync();

			var updated = _mapper.Map<StudentDto>(student);
			return ServiceResult<StudentDto>.Ok(updated);
		}

		public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
		{
			var student = await _uow.Students.GetByIdAsync(id);
			if (student == null) return ServiceResult<bool>.Fail("Student not found");

			// Soft delete
			student.IsDeleted = true;
			_uow.Students.Update(student);
			await _uow.SaveChangesAsync();

			return ServiceResult<bool>.Ok(true);
		}
	}
}
