using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Hawkins_SC.Abstract;
using Hawkins_SC.Concrate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hawkins.DataAccess.Context
{
	/// Hawkins School Management System ana veritabanı bağlam sınıfı.
	/// ASP.NET Identity entegrasyonu ile kullanıcı yönetimi sağlar.
	public class HawkinsDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		/// Constructor - Dependency Injection ile DbContextOptions alır
		/// <param name="options">DbContext yapılandırma seçenekleri</param>
		public HawkinsDbContext(DbContextOptions<HawkinsDbContext> options) : base(options)
		{
			// Connection string burada DEĞİL, Program.cs'te DI ile gelir
		}


		/// <summary>Öğrenciler tablosu</summary>
		public DbSet<Student> Students { get; set; }

		/// <summary>Öğretmenler tablosu</summary>
		public DbSet<Teacher> Teachers { get; set; }

		/// <summary>Dersler tablosu (Course definitions)</summary>
		public DbSet<Course> Courses { get; set; }

		/// <summary>Sınıflar tablosu (Class instances per semester)</summary>
		public DbSet<Class> Classes { get; set; }

		/// <summary>Kayıtlar tablosu (Student enrollments in classes)</summary>
		public DbSet<Enrollment> Enrollments { get; set; }

		/// <summary>Notlar tablosu</summary>
		public DbSet<Grade> Grades { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Base Identity tables oluştur (AspNetUsers, AspNetRoles, vb.)
			base.OnModelCreating(modelBuilder);

			// ✅ Tüm IEntityTypeConfiguration<T> sınıflarını otomatik yükle
			// StudentConfiguration, TeacherConfiguration, vb. ayrı dosyalarda
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HawkinsDbContext).Assembly);

			// ✅ Global Query Filter - Soft Delete
			// IsDeleted = true olan kayıtları otomatik filtrele (tüm sorgularda)
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				// BaseEntity<Guid> inheritance kontrolü
				if (typeof(BaseEntity<Guid>).IsAssignableFrom(entityType.ClrType))
				{
					var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
					var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity<Guid>.IsDeleted));
					var filter = System.Linq.Expressions.Expression.Lambda(
						System.Linq.Expressions.Expression.Not(property),
						parameter
					);
					modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
				}
			}
		}


		/// SaveChangesAsync override - Audit trail otomasyonu
		/// CreatedDate, ModifiedDate alanlarını otomatik doldurur
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			// Tüm eklenen veya güncellenen entity'leri bul
			var entries = ChangeTracker.Entries()
				.Where(e => e.Entity is BaseEntity<Guid> &&
						   (e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entry in entries)
			{
				var entity = (BaseEntity<Guid>)entry.Entity;

				if (entry.State == EntityState.Added)
				{
					entity.CreatedDate = DateTime.UtcNow;

					// Eğer IAuditableEntity interface'i varsa CreatedBy doldur
					if (entity is IAuditableEntity auditableEntity)
					{
						// TODO: HttpContext'ten gelen user bilgisi
						// auditableEntity.CreatedBy = _currentUserService.UserId;
					}
				}
				else if (entry.State == EntityState.Modified)
				{
					entity.ModifiedDate = DateTime.UtcNow;

					if (entity is IAuditableEntity auditableEntity)
					{
						// TODO: HttpContext'ten gelen user bilgisi
						// auditableEntity.ModifiedBy = _currentUserService.UserId;
					}
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		/// SaveChanges senkron versiyonu (async kullanımı tercih edilir)
		public override int SaveChanges()
		{
			return SaveChangesAsync().GetAwaiter().GetResult();
		}
	}
}