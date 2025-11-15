using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class ClassConfiguration : IEntityTypeConfiguration<Class>
	{
		public void Configure(EntityTypeBuilder<Class> builder)
		{
			builder.ToTable("Class");

			builder.HasKey(c => c.id);

			builder.Property(c => c.Semester)
				.IsRequired()
				.HasMaxLength(20)
				.HasConversion<string>(); // Enum to string conversion

			builder.Property(c => c.Year)
				.IsRequired();

			builder.Property(c => c.Capacity)
				.IsRequired()
				.HasDefaultValue(30);

			// Indexes
			builder.HasIndex(c => c.CourseId)
				.HasDatabaseName("IX_Classes_CourseId");

			builder.HasIndex(c => c.TeacherId)
				.HasDatabaseName("IX_Classes_TeacherId");

			// Composite index for semester queries
			builder.HasIndex(c => new { c.Semester, c.Year })
				.HasDatabaseName("IX_Classes_Semester_Year");

			// Foreign Key - Course
			builder.HasOne(c => c.Course)
				.WithMany(co => co.Classes)
				.HasForeignKey(c => c.CourseId)
				.OnDelete(DeleteBehavior.Restrict);

			// Foreign Key - Teacher (nullable)
			builder.HasOne(c => c.Teacher)
				.WithMany(t => t.Classes)
				.HasForeignKey(c => c.TeacherId)
				.OnDelete(DeleteBehavior.SetNull);

			// Navigation - Enrollments (1:N)
			builder.HasMany(c => c.Enrollments)
				.WithOne(e => e.Class)
				.HasForeignKey(e => e.ClassId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
