using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
		public HawkinsDbContext(DbContextOptions<HawkinsDbContext> options) : base(options)
		{
		}

		// Eğer DbContext DI ile yapılandırılmamışsa (ör. migration tool veya ad-hoc kullanım),
		// OnConfiguring içinde fallback connection string kullanılabilir.
		// Production/CI'de tercih edilmeyen bir yöntemdir; connection string'i konfigürasyondan sağlayın.
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// TODO: Move this connection string to appsettings.json or user secrets.
				optionsBuilder.UseSqlServer(@"Server=DESKTOP-L027AII\SQLEXPRESS;Database=HawkinsSC;User Id=sa;Password=1;TrustServerCertificate=True");
			}
		}

		/// <summary>Öğrenciler tablosu</summary>
		public DbSet<Student> Students { get; set; } = null!;

		/// <summary>Öğretmenler tablosu</summary>
		public DbSet<Teacher> Teachers { get; set; } = null!;

		/// <summary>Dersler tablosu (Course definitions)</summary>
		public DbSet<Course> Courses { get; set; } = null!;

		/// <summary>Sınıflar tablosu (Class instances per semester)</summary>
		public DbSet<Class> Classes { get; set; } = null!;

		/// <summary>Kayıtlar tablosu (Student enrollments in classes)</summary>
		public DbSet<Enrollment> Enrollments { get; set; } = null!;

		/// <summary>Notlar tablosu</summary>
		public DbSet<Grade> Grades { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Base Identity tables oluştur (AspNetUsers, AspNetRoles, vb.)
			base.OnModelCreating(modelBuilder);

			// Tüm IEntityTypeConfiguration<T> sınıflarını otomatik yükle
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(HawkinsDbContext).Assembly);

			// Global Query Filter - Soft Delete: BaseEntity<Guid> tipini kontrol et ve HasQueryFilter ekle
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(BaseEntity<Guid>).IsAssignableFrom(entityType.ClrType))
				{
					var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
					var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity<Guid>.IsDeleted));
					var notProperty = System.Linq.Expressions.Expression.Not(property);
					var lambda = System.Linq.Expressions.Expression.Lambda(notProperty, parameter);
					modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
				}
			}
		}

		/// SaveChangesAsync override - Audit trail otomasyonu
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

					if (entity is IAuditableEntity auditableEntity)
					{
						// TODO: HttpContext'ten gelen user bilgisi için _currentUserService veya benzeri bir servis bağlayın.
						// auditableEntity.CreatedBy = _currentUserService.UserId;
					}
				}
				else if (entry.State == EntityState.Modified)
				{
					entity.ModifiedDate = DateTime.UtcNow;

					if (entity is IAuditableEntity auditableEntity)
					{
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