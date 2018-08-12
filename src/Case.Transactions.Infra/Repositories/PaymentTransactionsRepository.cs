using System.Collections.Generic;
using System.Linq;
using Case.Transactions.Domain.Filters;
using Case.Transactions.Domain.Models;
using Case.Transactions.Domain.Queries;
using Case.Transactions.Infra.Builders;
using Dapper;

namespace Case.Transactions.Infra.Repositories
{
	public class PaymentTransactionsRepository : IPaymentTransactionsRepository
	{
		private readonly IDatabaseContext context;

		public PaymentTransactionsRepository(IDatabaseContext context)
			=> this.context = context;

		public QueryResult<PaymentTransaction> GetPaymentTransactions(PaymentTransactionFilters filters)
		{
			var (queryFilter, parameters) = new PaymentTransactionQueryBuilder(filters)
				.MerchantCnpjsQuery()
				.CheckoutCodesQuery()
				.AcquirerNamesQuery()
				.PaymentMethodsQuery()
				.CardBrandNamesQuery()
				.StatusQuery()
				.CreatedDateQuery()
				.AuthorizationDateQuery()
				.Build();

			if(string.IsNullOrEmpty(queryFilter))
			{
				return default(QueryResult<PaymentTransaction>);
			}

			var query = $@"SELECT MerchantCnpj
								, CheckoutCode
								, CipheredCardNumber
								, AmountInCents
								, Installments
								, AcquirerName
								, PaymentMethod
								, CardBrandName
								, Status
								, StatusInfo
								, CreatedAt
								, AcquirerAuthorizationDateTime 
								FROM PaymentTransaction 
								WHERE { queryFilter }";

			var results = this.context
				.Connection
				.Query<PaymentTransaction>(query, parameters)
				.Skip((filters.Offset - 1) * filters.Limit)
				.Take(filters.Limit);

			var queryResult = new QueryResult<PaymentTransaction>();
			queryResult.AddResults(results);

			return queryResult;
		}
	}
}
