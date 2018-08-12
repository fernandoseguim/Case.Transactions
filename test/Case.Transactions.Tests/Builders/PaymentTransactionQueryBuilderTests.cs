using Case.Transactions.Domain.Filters;
using Case.Transactions.Infra.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Case.Transactions.Tests.Builders
{
	[TestClass]
	public class PaymentTransactionQueryBuilderTests
	{
		[TestMethod]
		[Description("When filter has merchant cnpjs should return a query complement and dynamic parametes with Merchant Cnpj")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Merchant_Cnpj()
		{
			var filters = new PaymentTransactionFilters() { MerchantCnpjs = "13705903000110,09429293000100" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.MerchantCnpjsQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"MerchantCnpj in @MerchantCnpjs"));
			Assert.IsTrue(parameters.Get<string[]>("MerchantCnpjs").Any());
		}

		[TestMethod]
		[Description("When filter has checkout codes should return a query complement and dynamic parametes with checkout codes")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Checkout_Codes()
		{
			var filters = new PaymentTransactionFilters() { CheckoutCodes = "38687,38688" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.CheckoutCodesQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"CheckoutCode in @CheckoutCodes"));
			Assert.IsTrue(parameters.Get<string[]>("CheckoutCodes").Any());
		}

		[TestMethod]
		[Description("When filter has acquirer names should return a query complement and dynamic parametes with acquirer names")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Acquirer_Names()
		{
			var filters = new PaymentTransactionFilters() { AcquirerNames = "Stone, Cielo, Rede" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.AcquirerNamesQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"AcquirerName in @AcquirerNames"));
			Assert.IsTrue(parameters.Get<string[]>("AcquirerNames").Any());
		}

		[TestMethod]
		[Description("When filter has payment methods should return a query complement and dynamic parametes with payment methods")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Payment_Methods()
		{
			var filters = new PaymentTransactionFilters() { PaymentMethods = "Crédito à Vista, Crédito parcelado" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.PaymentMethodsQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"PaymentMethod in @PaymentMethods"));
			Assert.IsTrue(parameters.Get<string[]>("PaymentMethods").Any());
		}

		[TestMethod]
		[Description("When filter has card brand names should return a query complement and dynamic parametes with card brand names")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Card_Brand_Names()
		{
			var filters = new PaymentTransactionFilters() { CardBrandNames = "Visa, Mastercard, Elo" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.CardBrandNamesQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"CardBrandName in @CardBrandNames"));
			Assert.IsTrue(parameters.Get<string[]>("CardBrandNames").Any());
		}

		[TestMethod]
		[Description("When filter has status should return a query complement and dynamic parametes with status")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_Status()
		{
			var filters = new PaymentTransactionFilters() { Status = "Autorizado" };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.StatusQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"Status in @Status"));
			Assert.IsTrue(parameters.Get<string[]>("Status").Any());
		}

		[TestMethod]
		[Description("When filter has only created date start return a query complement and dynamic parametes with created date start and created date end with 24 hours after")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_CreatedDateStart_and_CreatedDateEnd_with_24_hours_after()
		{
			var filters = new PaymentTransactionFilters() { CreatedDateStart = DateTime.Parse("2018-03-01") };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.CreatedDateQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"CreatedAt BETWEEN @CreatedDateStart AND @CreatedDateEnd"));
			Assert.IsTrue(parameters.Get<DateTime>("CreatedDateStart") == DateTime.Parse("2018-03-01"));
			Assert.IsTrue(parameters.Get<DateTime>("CreatedDateEnd") == DateTime.Parse("2018-03-01").AddHours(24));
		}

		[TestMethod]
		[Description("When filter has created date start and created date end return a query complement and dynamic parametes with exactly created date start and created date end")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_exactly_CreatedDateStart_and_CreatedDateEnd()
		{
			var filters = new PaymentTransactionFilters() { CreatedDateStart = DateTime.Parse("2018-03-01"), CreatedDateEnd = DateTime.Parse("2018-03-11") };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.CreatedDateQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"CreatedAt BETWEEN @CreatedDateStart AND @CreatedDateEnd"));
			Assert.IsTrue(parameters.Get<DateTime>("CreatedDateStart") == DateTime.Parse("2018-03-01"));
			Assert.IsTrue(parameters.Get<DateTime>("CreatedDateEnd") == DateTime.Parse("2018-03-11"));
		}

		[TestMethod]
		[Description("When filter has only authorization date start return a query complement and dynamic parametes with authorization date start and authorization date end with 24 hours after")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_AuthorizationDateStart_and_AuthorizationDateEnd_with_24_hours_after()
		{
			var filters = new PaymentTransactionFilters() { AuthorizationDateStart = DateTime.Parse("2018-03-01") };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.AuthorizationDateQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"AcquirerAuthorizationDateTime BETWEEN @AuthorizationDateStart AND @AuthorizationDateEnd"));
			Assert.IsTrue(parameters.Get<DateTime>("AuthorizationDateStart") == DateTime.Parse("2018-03-01"));
			Assert.IsTrue(parameters.Get<DateTime>("AuthorizationDateEnd") == DateTime.Parse("2018-03-01").AddHours(24));
		}

		[TestMethod]
		[Description("When filter has authorization date and authorization date end return a query complement and dynamic parametes with exactly authorization date start and authorization date end")]
		public void Should_return_a_query_complement_and_dynamic_parametes_with_exactly_AuthorizationDateStart_and_AuthorizationDateEnd()
		{
			var filters = new PaymentTransactionFilters() { AuthorizationDateStart = DateTime.Parse("2018-03-01"), AuthorizationDateEnd = DateTime.Parse("2018-03-11") };
			var (queryComplement, parameters) = new PaymentTransactionQueryBuilder(filters)
				.AuthorizationDateQuery()
				.Build();

			Assert.IsTrue(queryComplement.Contains(@"AcquirerAuthorizationDateTime BETWEEN @AuthorizationDateStart AND @AuthorizationDateEnd"));
			Assert.IsTrue(parameters.Get<DateTime>("AuthorizationDateStart") == DateTime.Parse("2018-03-01"));
			Assert.IsTrue(parameters.Get<DateTime>("AuthorizationDateEnd") == DateTime.Parse("2018-03-11"));
		}
	}
}