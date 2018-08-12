using Case.Transactions.Domain.Filters;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace Case.Transactions.Infra.Builders
{
	public class PaymentTransactionQueryBuilder
	{
		private readonly PaymentTransactionFilters filters;
		private readonly DynamicParameters parameters;

		private string queryComplement = "";
		
		public PaymentTransactionQueryBuilder(PaymentTransactionFilters filters)
		{
			this.filters = filters ?? throw new ArgumentNullException(nameof(filters));
			this.parameters = new DynamicParameters();
		}

		public PaymentTransactionQueryBuilder MerchantCnpjsQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.MerchantCnpjs))
			{
				this.FormatQuery($"MerchantCnpj in @MerchantCnpjs");
				this.parameters.AddDynamicParams(new { MerchantCnpjs = GetFilterItems(this.filters.MerchantCnpjs) } );
			}
			return this;
		}

		public PaymentTransactionQueryBuilder CheckoutCodesQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.CheckoutCodes))
			{
				this.FormatQuery($"CheckoutCode in @CheckoutCodes");
				this.parameters.AddDynamicParams(new { CheckoutCodes = GetFilterItems(this.filters.CheckoutCodes) });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder AcquirerNamesQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.AcquirerNames))
			{
				this.FormatQuery($"AcquirerName in @AcquirerNames");
				this.parameters.AddDynamicParams(new { AcquirerNames = GetFilterItems(this.filters.AcquirerNames) });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder PaymentMethodsQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.PaymentMethods))
			{
				this.FormatQuery($"PaymentMethod in @PaymentMethods");
				this.parameters.AddDynamicParams(new { PaymentMethods = GetFilterItems(this.filters.PaymentMethods) });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder CardBrandNamesQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.CardBrandNames))
			{
				this.FormatQuery($"CardBrandName in @CardBrandNames");
				this.parameters.AddDynamicParams(new { CardBrandNames = GetFilterItems(this.filters.CardBrandNames) });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder StatusQuery()
		{
			if (!string.IsNullOrWhiteSpace(this.filters.Status))
			{
				this.FormatQuery($"Status in @Status");
				this.parameters.AddDynamicParams(new { Status = GetFilterItems(this.filters.Status) });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder CreatedDateQuery()
		{
			if (this.filters.CreatedDateStart != default(DateTime))
			{
				if(this.filters.CreatedDateEnd == default(DateTime))
				{
					this.filters.CreatedDateEnd = this.filters.CreatedDateStart.AddHours(24);
				}

				this.FormatQuery($"CreatedAt BETWEEN @CreatedDateStart AND @CreatedDateEnd");
				this.parameters.AddDynamicParams(new { this.filters.CreatedDateStart, this.filters.CreatedDateEnd });
			}
			return this;
		}

		public PaymentTransactionQueryBuilder AuthorizationDateQuery()
		{
			if (this.filters.AuthorizationDateStart != default(DateTime))
			{
				if (this.filters.AuthorizationDateEnd == default(DateTime))
				{
					this.filters.AuthorizationDateEnd = this.filters.AuthorizationDateStart.AddHours(24);
				}

				this.FormatQuery($"AcquirerAuthorizationDateTime BETWEEN @AuthorizationDateStart AND @AuthorizationDateEnd");
				this.parameters.AddDynamicParams(new { this.filters.AuthorizationDateStart, this.filters.AuthorizationDateEnd });
			}
			return this;
		}

		public (string, DynamicParameters) Build() => 
			(this.queryComplement, this.parameters);

		private void FormatQuery(string queryItem)
		{
			if (this.queryComplement != "") { this.queryComplement = $"{this.queryComplement} AND"; }
			this.queryComplement = $"{this.queryComplement} {queryItem}";
		}

		private static string[] GetFilterItems(string filterItems) => filterItems.Split(',').ToArray();
	}
}
