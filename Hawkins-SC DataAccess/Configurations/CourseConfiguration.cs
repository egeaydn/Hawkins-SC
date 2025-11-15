using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.ToTable("Course");

			builder.HasKey(c => c.id);

			builder.Property(c => c.CourseCode)
				.IsRequired()
				.HasMaxLength(20);

			builder.Property(c => c.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(c => c.Description)
				.HasMaxLength(1000);

			builder.Property(c => c.Credits)
				.IsRequired()
				.HasDefaultValue(3);

			// Index - Course Code must be unique
			builder.HasIndex(c => c.CourseCode)
				.IsUnique()
				.HasDatabaseName("IX_Courses_Code");

			// Navigation - Classes (1:N)
			builder.HasMany(c => c.Classes)
				.WithOne(cl => cl.Course)
				.HasForeignKey(cl => cl.CourseId)
				.OnDelete(DeleteBehavior.Restrict); // Course silinince Class'lar silinmesin
		}
	}
}
