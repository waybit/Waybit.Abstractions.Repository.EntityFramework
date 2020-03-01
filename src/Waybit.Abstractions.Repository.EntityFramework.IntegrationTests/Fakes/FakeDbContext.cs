using System.Data.Common;
using System.Data.Entity;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes
{
	/// <summary>
	/// Testing entity framework database context
	/// </summary>
	public class FakeDbContext : DbContext
	{
		/// <inheritdoc />
		public FakeDbContext(DbConnection existingConnection)
			: base(existingConnection, false)
		{
		}

		/// <summary>
		/// Testing database set
		/// </summary>
		public DbSet<FakeAggregateRoot> FakeAggregateRoots { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new FakeAggregateRootEntityTypeConfiguration());
		}
	}
}