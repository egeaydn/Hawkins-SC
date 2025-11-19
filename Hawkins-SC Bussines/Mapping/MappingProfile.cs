using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hawkins_SC.Concrate;
using Hawkins_SC_Bussines.DTOs;

namespace Hawkins_SC_Bussines.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Student, StudentDto>().ReverseMap();
			CreateMap<CreateStudentDto, Student>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
				.ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
			// Add mappings for Teacher, Course, Class, Enrollment, Grade...
		}
	}
}
