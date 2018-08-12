using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Case.Transactions.Domain.Filters;
using Case.Transactions.Domain.Models;
using Case.Transactions.Domain.Queries;
using Case.Transactions.Infra.Repositories;

namespace Case.Transactions.Api.Controllers
{
	[Route("api/[controller]")]
	public class TransactionsController : Controller
	{
		private readonly IPaymentTransactionsRepository repository;
		public TransactionsController([FromServices] IPaymentTransactionsRepository repository) => 
			this.repository = repository;

		// GET api/values
		[HttpGet()]
		public IActionResult Get([FromQuery] PaymentTransactionFilters filters)
		{
			try
			{
				var response = this.repository.GetPaymentTransactions(filters);

				if(response == default(QueryResult<PaymentTransaction>))
				{
					return this.BadRequest();
				}

				return this.Ok(response);
			}
			catch(Exception ex)
			{
				return this.StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
			
		} 
			
	}
}
