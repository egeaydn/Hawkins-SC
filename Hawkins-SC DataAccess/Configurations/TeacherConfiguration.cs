using System;
using Hawkins_SC.Concrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hawkins_SC_DataAccess.Configurations
{
	public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			// Table name
			builder.ToTable("Teachers");

			// Primary Key
			builder.HasKey(t => t.id);

			// Properties
			builder.Property(t => t.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.LastName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(t => t.Email)
				.IsRequired()
				.HasMaxLength(256);

			// ✅ Teacher'da HireDate var (EnrollmentDate değil!)
			builder.Property(t => t.HireDate)
				.IsRequired();

			builder.Property(t => t.IsActive)
				.IsRequired()
				.HasDefaultValue(true);

			// Indexes
			builder.HasIndex(t => t.Email)
				.IsUnique()
				.HasDatabaseName("IX_Teachers_Email");

			builder.HasIndex(t => t.IdentityUserId)
				.IsUnique()
				.HasDatabaseName("IX_Teachers_IdentityUserId");

			// Foreign Key - ApplicationUser (1:1 relationship)
			builder.HasOne(t => t.User)
				.WithOne()
				.HasForeignKey<Teacher>(t => t.IdentityUserId)
				.OnDelete(DeleteBehavior.Cascade);

			// ✅ Navigation - Classes (1:N) - Teacher'ın verdiği dersler
			builder.HasMany(t => t.Classes)
				.WithOne(c => c.Teacher)
				.HasForeignKey(c => c.TeacherId)
				.OnDelete(DeleteBehavior.SetNull);

			// ✅ Navigation - GivenGrades (1:N) - Teacher'ın verdiği notlar
			builder.HasMany(t => t.GivenGrades)
				.WithOne(g => g.GivenByTeacher)
				.HasForeignKey(g => g.GivenByTeacherId)
				.OnDelete(DeleteBehavior.SetNull);

			// Audit fields
			builder.Property(t => t.CreatedBy).HasMaxLength(256);
			builder.Property(t => t.ModifiedBy).HasMaxLength(256);
			builder.Property(t => t.DeletedBy).HasMaxLength(256);
		}
	}
}