using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC_DataAccess.Repositories.Abstract;
using Hawkins_SC_DataAccess.Repositories.Concrete;
using Hawkins_SC_DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;

namespace Hawkins_SC_DataAccess.Extension
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBusiness(this IServiceCollection services)
		{
			// UnitOfWork
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Repositories (register if you want DI for repos directly)
			services.AddScoped<IStudentRepository, StudentRepository>();

			// Business services
			services.AddScoped<IStudentService, StudentService>();
			// add other services similarly: ITeacherService, IClassService, etc.

			// AutoMapper profile registration (optional here - Program.cs can do it too)
			services.AddAutoMapper(typeof(MappingProfile));

			// Validators registration (optional - Program.cs can do it too)
			services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

			return services;
		}
	}
}
