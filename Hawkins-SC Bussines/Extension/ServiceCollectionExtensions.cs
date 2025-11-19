using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Hawkins_SC_Bussines.Contracts;
using Hawkins_SC_Bussines.DTOs;
using Hawkins_SC_Bussines.Services;
using Hawkins_SC_Bussines.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Hawkins_SC_Bussines.Extension
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBusiness(this IServiceCollection services)
		{
			// Services
			services.AddScoped<IStudentService, StudentService>();

			// AutoMapper
			services.AddAutoMapper(typeof(Mapping.MappingProfile));

			// Validators
			services.AddScoped<IValidator<CreateStudentDto>, CreateStudentValidator>();

			return services;
		}
	}
}
