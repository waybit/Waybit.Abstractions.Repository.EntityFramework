using System.Collections.Generic;
using Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Fakes;

namespace Waybit.Abstractions.Repository.EntityFramework.IntegrationTests.Comparers
{
	/// <summary>
	/// Fake aggregate root comparer
	/// </summary>
	public sealed class FakeAggregateRootEqualityComparer : IEqualityComparer<FakeAggregateRoot>
	{
		/// <inheritdoc />
		public bool Equals(FakeAggregateRoot x, FakeAggregateRoot y)
		{
			if (ReferenceEquals(x, y))
			{
				return true;
			}

			if (ReferenceEquals(x, null))
			{
				return false;
			}

			if (ReferenceEquals(y, null))
			{
				return false;
			}

			if (x.GetType() != y.GetType())
			{
				return false;
			}

			return x.Name == y.Name;
		}

		/// <inheritdoc />
		public int GetHashCode(FakeAggregateRoot obj)
		{
			return (obj.Name != null ? obj.Name.GetHashCode() : 0);
		}
	}
}