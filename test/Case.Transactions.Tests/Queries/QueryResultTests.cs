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
		private readonly IEnumerable<Transaction> results;

	    public QueryResultTests()
	    {
		    this.results = new List<Transaction>()
		    {
				new Transaction()
		    };
	    }

	    [TestMethod]
		[Description("When instance query result with transaction type and add a result list the results should contains transaction items")]
	    public void When_add_results_shoul_contains_transaction_items()
	    {
			var queryResult = new QueryResult<Transaction>();
			queryResult.AddResults(this.results);

			Assert.IsTrue(queryResult.Results.ToList().Contains(this.results.First()));
	    }
    }
}
