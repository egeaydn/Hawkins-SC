using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using FluentAssertions;
using Hawkins_SC.Concrate;
using Hawkins_SC_Bussines.DTOs;
using Hawkins_SC_Bussines.Mapping;
using Hawkins_SC_Bussines.Services;
using Hawkins_SC_DataAccess.Repositories.Abstract;
using Hawkins_SC_DataAccess.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Hawkins_SC_Bussines.Test
{
	public class StudentServiceTests
	{
		public async Task CreateAsync_ShouldReturnCreatedStudent()
		{
			// Arrange
			var uowMock = new Mock<IUnitOfWork>();
			var studentsRepoMock = new Mock<IStudentRepository>();
			uowMock.SetupGet(x => x.Students).Returns(studentsRepoMock.Object);

			studentsRepoMock
				.Setup(x => x.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Student, bool>>>()))
				.ReturnsAsync(false);

			studentsRepoMock
				.Setup(x => x.AddAsync(It.IsAny<Student>()))
				.Returns(Task.CompletedTask);

			// AutoMapper oluşturma via DI (bu yöntem çakışmaları önler)
			var services = new ServiceCollection();
			services.AddAutoMapper(typeof(MappingProfile)); // MappingProfile sınıfının doğru namespace ve adı ile eşleştir
			var provider = services.BuildServiceProvider();
			var mapper = provider.GetRequiredService<IMapper>();

			var validator = new Hawkins_SC_Bussines.Validators.CreateStudentValidator();

			var service = new StudentService(uowMock.Object, mapper, validator);

			var dto = new CreateStudentDto
			{
				IdentityUserId = Guid.NewGuid().ToString(),
				FirstName = "Foo",
				LastName = "Bar",
				Email = "foo@x.com"
			};

			// Act
			var result = await service.CreateAsync(dto);

			// Assert
			result.Success.Should().BeTrue();
			result.Data.Should().NotBeNull();
			result.Data!.FirstName.Should().Be("Foo");
		}
	}
}
