using System.Collections.Generic;
using AutoFixture;
using Case.Transactions.Domain.Models;
using Case.Transactions.Infra;
using Case.Transactions.Infra.Repositories;
using NSubstitute;

namespace Case.Transactions.Tests.Repositories
{
	public abstract class RepositoryTests<T> where T : class 
	{
		protected RepositoryTests(IEnumerable<T> objects)
		{
			var fakeDatabase = new InMemoryDatabase();
			fakeDatabase.Insert(objects);

			this.Context = Substitute.For<IDatabaseContext>();
			this.Context.Connection.Returns(fakeDatabase.OpenConnection());
		}

		public IDatabaseContext Context { get; }
	}
}