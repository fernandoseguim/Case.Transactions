using System.Data;

namespace Case.Transactions.Infra
{
	public class TransactionsDatabaseContext : IDatabaseContext
	{
		public TransactionsDatabaseContext(IDbConnection connection)
		{
			this.Connection = connection;
			this.Connection.Open();
		}

		public IDbConnection Connection { get; }

		public void Dispose()
		{
			if (this.Connection.State != ConnectionState.Closed)
			{
				this.Connection.Close();
			}
		}
	}
}
