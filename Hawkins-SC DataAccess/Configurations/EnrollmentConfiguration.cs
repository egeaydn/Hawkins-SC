using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
	{
		public void Configure(EntityTypeBuilder<Enrollment> builder)
		{
			builder.ToTable("Enrollments");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.EnrollDate)
				.IsRequired()
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(e => e.Status)
				.IsRequired()
				.HasMaxLength(20)
				.HasConversion<string>(); // Enum to string

			// ⚠️ CRITICAL: UNIQUE CONSTRAINT
			// Bir öğrenci aynı sınıfa 2 kez kayıt olamaz!
			builder.HasIndex(e => new { e.StudentId, e.ClassId })
				.IsUnique()
				.HasDatabaseName("IX_Enrollments_Student_Class_Unique");

			// Indexes
			builder.HasIndex(e => e.StudentId)
				.HasDatabaseName("IX_Enrollments_StudentId");

			builder.HasIndex(e => e.ClassId)
				.HasDatabaseName("IX_Enrollments_ClassId");

			builder.HasIndex(e => e.Status)
				.HasDatabaseName("IX_Enrollments_Status");

			// Foreign Key - Student
			builder.HasOne(e => e.Student)
				.WithMany(s => s.Enrollments)
				.HasForeignKey(e => e.StudentId)
				.OnDelete(DeleteBehavior.Cascade);

			// Foreign Key - Class
			builder.HasOne(e => e.Class)
				.WithMany(c => c.Enrollments)
				.HasForeignKey(e => e.ClassId)
				.OnDelete(DeleteBehavior.Cascade);

			// Navigation - Grades (1:N)
			builder.HasMany(e => e.Grades)
				.WithOne(g => g.Enrollment)
				.HasForeignKey(g => g.EnrollmentId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
