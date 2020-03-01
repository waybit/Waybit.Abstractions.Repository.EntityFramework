using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests
{
	public class RepositoryTests : RepositoryTestsFixture
	{
		[Test]
		public async Task Can_get_all_entities_count_equality()
		{
			// Arrange
			const int expected = 10;
			base.EnsureHasFakeAggregateRoots(expected);
			
			// Act
			IEnumerable<FakeAggregateRoot> fakes = await _repository.GetAllAsync(_cancellationToken);
			List<FakeAggregateRoot> actual = fakes?.ToList();
			
			// Assert
			actual.ShouldNotBeNull();
			actual.Count.ShouldBe(expected);
		}
		
		[Test]
		public async Task Can_get_all_entities_comparable()
		{
			// Arrange
			IReadOnlyCollection<FakeAggregateRoot> expected = base.EnsureHasFakeAggregateRoots(5);
			
			// Act
			IEnumerable<FakeAggregateRoot> fakes = await _repository.GetAllAsync(_cancellationToken);
			List<FakeAggregateRoot> actual = fakes?.ToList();
			
			// Assert
			actual.ShouldNotBeNull();
			actual.SequenceEqual(expected, _fakeAggregateRootEqualityComparer).ShouldBeTrue();
		}
		
		[Test]
		public async Task Can_get_by_id()
		{
			// Arrange
			const int fakeAggregateRootId = 1;
			FakeAggregateRoot expected = base.EnsureHasFakeAggregateRoot();
			
			// Act
			FakeAggregateRoot actual = await _repository.GetByIdAsync(
				fakeAggregateRootId,
				_cancellationToken);
			
			// Assert
			actual.ShouldNotBeNull();
			_fakeAggregateRootEqualityComparer.Equals(actual, expected).ShouldBeTrue();
		}
		
		[Test]
		public async Task Can_get_by_id_not_found()
		{
			// Arrange
			const int notFoundId = 11;
			base.EnsureHasFakeAggregateRoot();
			
			// Act
			FakeAggregateRoot actual = await _repository.GetByIdAsync(
				notFoundId,
				_cancellationToken);
			
			// Assert
			actual.ShouldBeNull();
		}

		[Test]
		public async Task Can_add_and_commit()
		{
			// Arrange
			const string addedName = "expectedName";
			var added = new FakeAggregateRoot(addedName);
			
			// Act
			await _repository.AddAsync(added, _cancellationToken);
			await _dbContext.SaveChangesAsync();
			
			// Assert
			FakeAggregateRoot expected = await _dbContext
				.Set<FakeAggregateRoot>()
				.FirstAsync(f => f.Name == added.Name, _cancellationToken);
			
			expected.ShouldNotBeNull();
			expected.Name.ShouldBe(addedName);
		}
		
		[Test]
		public async Task Can_add_and_commit_conflict()
		{
			// Arrange
			FakeAggregateRoot added = base.EnsureHasFakeAggregateRoot();
			var newFake = new FakeAggregateRoot("newFake");
			
			// Act
			int actual = await _repository.AddAsync(newFake, _cancellationToken);
			
			// Assert
			actual.ShouldNotBe(added.Id);
		}
		
		[Test]
		public async Task Can_update_and_commit()
		{
			// Arrange
			FakeAggregateRoot existed = base.EnsureHasFakeAggregateRoot();
			const string updatedName = "updatedName";
			
			// Act
			existed.ChangeName(updatedName);

			await _repository.UpdateAsync(existed, _cancellationToken);
			
			// Assert
			FakeAggregateRoot expected = await _dbContext
				.Set<FakeAggregateRoot>()
				.SingleAsync(f => f.Id == existed.Id, _cancellationToken);
			
			expected.Name.ShouldBe(updatedName);
		}
	}
}
