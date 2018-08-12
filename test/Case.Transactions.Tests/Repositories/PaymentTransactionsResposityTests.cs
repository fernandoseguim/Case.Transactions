using Case.Transactions.Domain.Filters;
using Case.Transactions.Domain.Models;
using Case.Transactions.Infra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Case.Transactions.Tests.Repositories
{
	[TestClass]
	public class PaymentTransactionsResposityTests : RepositoryTests<PaymentTransaction>
	{
		private readonly IPaymentTransactionsRepository repository;
		private readonly PaymentTransactionFilters filters;

		public PaymentTransactionsResposityTests() : base(new List<PaymentTransaction>() { new PaymentTransaction() { MerchantCnpj = "13705903000110" } })
		{
			this.filters = new PaymentTransactionFilters()
			{
				MerchantCnpjs = "13705903000110,09429293000100"
			};
			this.repository = new PaymentTransactionsRepository(this.Context);
		}


		[TestMethod]
		[Description("When try get transaction througth filters should returns a query result with payment transaction items")]
		public void Should_returns_a_query_result_with_payment_transaction_items()
		{
			var queryResult = this.repository.GetPaymentTransactions(this.filters);

			Assert.IsTrue(queryResult.Results.Any());
		}
	}
}