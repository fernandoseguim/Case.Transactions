using System.Collections.Generic;
using System.Linq;
using Case.Transactions.Domain.Models;
using Case.Transactions.Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case.Transactions.Tests.Queries
{
	[TestClass]
	public class QueryResultTests
	{
		private readonly IEnumerable<PaymentTransaction> results;

		public QueryResultTests()
		{
			this.results = new List<PaymentTransaction>()
			{
				new PaymentTransaction()
			};
		}

		[TestMethod]
		[Description("When instance query result with transaction type and add a result list the results should contains payment transaction items")]
		public void Should_contains_payment_transaction_items()
		{
			var queryResult = new QueryResult<PaymentTransaction>();
			queryResult.AddResults(this.results);

			Assert.IsTrue(queryResult.Results.ToList().Contains(this.results.First()));
		}
	}
}
