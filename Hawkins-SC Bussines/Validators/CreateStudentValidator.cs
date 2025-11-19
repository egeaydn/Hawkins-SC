using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Hawkins_SC_Bussines.DTOs;

namespace Hawkins_SC_Bussines.Validators
{
	public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
	{
		public CreateStudentValidator()
		{
			RuleFor(x => x.IdentityUserId).NotEmpty();
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
			RuleFor(x => x.EnrollmentDate).LessThanOrEqualTo(DateTime.UtcNow);
		}
	}
}
