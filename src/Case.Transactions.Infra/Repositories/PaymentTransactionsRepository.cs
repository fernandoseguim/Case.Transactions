using Case.Transactions.Domain.Models;
using Case.Transactions.Domain.Queries;
using Dapper;

namespace Case.Transactions.Infra.Repositories
{
	public class PaymentTransactionsRepository : IPaymentTransactionsRepository
	{
		private readonly IDatabaseContext context;

		public PaymentTransactionsRepository(IDatabaseContext context)
			=> this.context = context;

		public QueryResult<PaymentTransaction> GetPaymentTransactions(string filter)
		{
			const string QUERY = @"SELECT merchantCnpj
								, checkoutCode
								, cipheredCardNumber
								, amountInCents
								, installments
								, acquirerName
								, paymentMethod
								, cardBrandName
								, status
								, statusInfo
								, CreatedAt
								, AcquirerAuthorizationDateTime 
								FROM PaymentTransaction WHERE merchantCnpj = @merchantCnpj";

			var results = this.context.Connection.Query<PaymentTransaction>(QUERY, new { merchantCnpj = filter });

			var queryResult = new QueryResult<PaymentTransaction>();
			queryResult.AddResults(results);

			return queryResult;
		}
	}
}
