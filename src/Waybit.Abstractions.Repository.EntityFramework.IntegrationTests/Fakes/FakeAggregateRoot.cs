using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes
{
	/// <summary>
	/// Testing aggregate root
	/// </summary>
	public class FakeAggregateRoot : Entity<int>, IAggregateRoot 
	{
		/// <summary>
		/// Initialize instance of <see cref="FakeAggregateRoot"/>
		/// </summary>
		protected FakeAggregateRoot() : base(default) { }

		/// <summary>
		/// Initialize instance of <see cref="FakeAggregateRoot"/>
		/// </summary>
		/// <param name="name">Name</param>
		public FakeAggregateRoot(string name)
			: this()
		{
			Name = name;
		}

		/// <summary>
		/// Testing string property
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Change name
		/// </summary>
		/// <param name="name">Name</param>
		public void ChangeName(string name)
		{
			Name = name;
		}
	}
}
