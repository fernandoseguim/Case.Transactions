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

		public PaymentTransactionsResposityTests() : base(new List<PaymentTransaction>() { new PaymentTransaction() { MerchantCnpj = "36075268855" } }) => 
			this.repository = new PaymentTransactionsRepository(this.Context);

		[TestMethod]
		[Description("When try get transaction througth filters should returns a query result with payment transaction items")]
		public void Should_returns_a_query_result_with_payment_transaction_items()
		{
			var queryResult = this.repository.GetPaymentTransactions("36075268855");

			Assert.IsTrue(queryResult.Results.Any());
		}
	}
}
