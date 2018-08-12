using System;
using System.Collections.Generic;

namespace Case.Transactions.Domain.Queries
{
    public class QueryResult<T> where T : class
    {
	    private readonly List<T> results;

		public QueryResult() 
			=> this.results = new List<T>();
		
	    public IReadOnlyCollection<T> Results 
		    => this.results;

	    public void AddResults(IEnumerable<T> queryResult) 
		    => this.results.AddRange(queryResult);
    }
}
