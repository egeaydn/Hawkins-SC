using System;
using System.Collections.Generic;
using System.Text;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class StudentConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			// hangi tablodan çekeceğimizi belirtiyoruz
			builder.ToTable("Students");

			// Primary Key tanımlaması
			builder.HasKey(s => s.Id);

			// Propertiler
			builder.Property(s => s.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.LastName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(s => s.Email)
				.IsRequired()
				.HasMaxLength(256);

			builder.Property(s => s.EnrollmentDate)
				.IsRequired();

			builder.Property(s => s.IsActive)
				.IsRequired()
				.HasDefaultValue(true);

			builder.HasIndex(s => s.Email)
				.IsUnique()
				.HasDatabaseName("IX_Students_Email");

			builder.HasIndex(s => s.IdentityUserId)
				.IsUnique()
				.HasDatabaseName("IX_Students_IdentityUserId");

			// Foreign Key - ApplicationUser (1:1 relationship)
			builder.HasOne(s => s.User)
				.WithOne()
				.HasForeignKey<Student>(s => s.IdentityUserId)
				.OnDelete(DeleteBehavior.Cascade);

			// Navigation - Enrollments (1:N)
			builder.HasMany(s => s.Enrollments)
				.WithOne(e => e.Student)
				.HasForeignKey(e => e.StudentId)
				.OnDelete(DeleteBehavior.Cascade);

			// Audit fields
			builder.Property(s => s.CreatedBy).HasMaxLength(256);
			builder.Property(s => s.ModifiedBy).HasMaxLength(256);
			builder.Property(s => s.DeletedBy).HasMaxLength(256);
		}
	}
}
