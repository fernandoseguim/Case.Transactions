using System;
using System.Collections.Generic;
using System.Linq;

namespace Case.Transactions.Domain.Filters
{
	public class PaymentTransactionFilters
	{
		public string MerchantCnpjs { get; set; }

		public string CheckoutCodes { get; set; }
		
		public string AcquirerNames { get; set; }
		public string PaymentMethods { get; set; }
		public string CardBrandNames { get; set; }
		public string Status { get; set; }
		
		public DateTime CreatedDateStart { get; set; }
		public DateTime CreatedDateEnd { get; set; }
		public DateTime AuthorizationDateStart { get; set; }

		public DateTime AuthorizationDateEnd { get; set; }

		public int Offset { get; set; } = 1;

		public int Limit { get; set; } = 20;
	}
}
