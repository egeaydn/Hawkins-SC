using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class GradeConfiguration : IEntityTypeConfiguration<Grade>
	{
		public void Configure(EntityTypeBuilder<Grade> builder)
		{
			builder.ToTable("Grades");

			builder.HasKey(g => g.id);

			// Decimal precision for grades (0.00 - 100.00)
			builder.Property(g => g.GradeValue)
				.IsRequired()
				.HasColumnType("decimal(5,2)");

			builder.Property(g => g.GradeType)
				.IsRequired()
				.HasMaxLength(50)
				.HasConversion<string>(); // Enum to string

			builder.Property(g => g.DateGiven)
				.IsRequired()
				.HasDefaultValueSql("GETUTCDATE()");

			// Indexes
			builder.HasIndex(g => g.EnrollmentId)
				.HasDatabaseName("IX_Grades_EnrollmentId");

			builder.HasIndex(g => g.GivenByTeacherId)
				.HasDatabaseName("IX_Grades_TeacherId");

			builder.HasIndex(g => new { g.EnrollmentId, g.GradeType })
				.HasDatabaseName("IX_Grades_Enrollment_Type");

			// Foreign Key - Enrollment
			builder.HasOne(g => g.Enrollment)
				.WithMany(e => e.Grades)
				.HasForeignKey(g => g.EnrollmentId)
				.OnDelete(DeleteBehavior.Cascade);

			// Foreign Key - Teacher (nullable)
			builder.HasOne(g => g.GivenByTeacher)
				.WithMany(t => t.GivenGrades)
				.HasForeignKey(g => g.GivenByTeacherId)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
