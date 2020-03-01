using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using Effort;
using NUnit.Framework;
using Waybit.Abstractions.Domain;
using Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Comparers;
using Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests
{
	/// <summary>
	/// Test fixture
	/// </summary>
	public class RepositoryTestsFixture
	{
		protected FakeDbContext _dbContext;

		protected IRepository<FakeAggregateRoot, int> _repository;
		protected static readonly CancellationToken _cancellationToken 
			= CancellationToken.None;
		protected static readonly FakeAggregateRootEqualityComparer _fakeAggregateRootEqualityComparer 
			= new FakeAggregateRootEqualityComparer();
		
		/// <summary>
		/// Before any test
		/// </summary>
		[SetUp]
		public void Setup()
		{
			DbConnection inMemoryConnection = DbConnectionFactory.CreateTransient();
			_dbContext = new FakeDbContext(inMemoryConnection);
			_repository = new FakeRepository(_dbContext);
		}

		/// <summary>
		/// After any test
		/// </summary>
		[TearDown]
		public void Down()
		{
			_dbContext.Dispose();
		}

		/// <summary>
		/// Ensures that there is fake aggregate roots with the specified count
		/// </summary>
		/// <param name="count">Count of fake aggregate roots</param>
		protected IReadOnlyCollection<FakeAggregateRoot> EnsureHasFakeAggregateRoots(int count)
		{
			for (int i = 0; i < count; i++)
			{
				var fakeAggregateRoot = new FakeAggregateRoot(GenerateRandomName());

				_dbContext
					.Set<FakeAggregateRoot>()
					.Add(fakeAggregateRoot);
			}

			_dbContext.SaveChanges();

			return _dbContext
				.Set<FakeAggregateRoot>()
				.ToList();
		}

		/// <summary>
		/// Ensures that is fake aggregate root
		/// </summary>
		protected FakeAggregateRoot EnsureHasFakeAggregateRoot()
		{
			var fakeAggregateRoot = new FakeAggregateRoot(GenerateRandomName());

			FakeAggregateRoot entry = _dbContext
				.Set<FakeAggregateRoot>()
				.Add(fakeAggregateRoot);

			_dbContext.SaveChanges();

			return entry;
		}

		private static string GenerateRandomName()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
