using System.Data.Entity.ModelConfiguration;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes
{
	/// <summary>
	/// Entity framework mapping
	/// </summary>
	public class FakeAggregateRootEntityTypeConfiguration
		: EntityTypeConfiguration<FakeAggregateRoot>
	{
		/// <inheritdoc />
		public FakeAggregateRootEntityTypeConfiguration()
		{
			this.ToTable("Fakes");

			this.HasKey(e => e.Id, primaryKey => primaryKey.HasName("Id"));
			
			this.Property(e => e.Name)
				.HasColumnName("Name")
				.IsRequired();
		}
	}
}