using Case.Transactions.Domain.Filters;
using Case.Transactions.Domain.Models;
using Case.Transactions.Domain.Queries;

namespace Case.Transactions.Infra.Repositories
{
	public interface IPaymentTransactionsRepository
	{
		QueryResult<PaymentTransaction> GetPaymentTransactions(PaymentTransactionFilters filters);
	}
}