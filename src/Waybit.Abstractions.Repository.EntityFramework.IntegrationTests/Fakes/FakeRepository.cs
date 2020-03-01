using System.Data.Entity;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes
{
	/// <summary>
	/// Testing instance of repository
	/// </summary>
	internal class FakeRepository : Repository<FakeAggregateRoot, int>
	{
		/// <inheritdoc />
		public FakeRepository(DbContext dbContext)
			: base(dbContext)
		{
		}
	}
}