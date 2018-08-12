using System;
using System.Data;

namespace Case.Transactions.Infra
{
	public interface IDatabaseContext : IDisposable
	{
		IDbConnection Connection { get; }
	}
}