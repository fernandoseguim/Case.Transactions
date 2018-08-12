using System.Data;
using System.Data.SqlClient;
using Case.Transactions.Infra;
using Case.Transactions.Infra.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Case.Transactions.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
			=> this.Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IDbConnection>(connection => new SqlConnection(this.Configuration.GetConnectionString("TransactionsDatabaseContext")));
			services.AddTransient<IDatabaseContext, TransactionsDatabaseContext>();
			services.AddTransient<IPaymentTransactionsRepository, PaymentTransactionsRepository>();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
