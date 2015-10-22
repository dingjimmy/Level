using System.Data;
using System.Data.Common;

namespace Level.RelationalPersistance
{

    /// <summary>
    /// Creates database objects.
    /// </summary>
    public interface IDatabaseFactory
    {

        /// <summary>
        /// Creates a database connection using the specified connection string.
        /// </summary>
        IDbConnection CreateDbConnection(string connectionString);

    }


    /// <summary>
    /// Wrapper for <see cref="DbProviderFactory"/>.
    /// </summary>
    public class DatabaseFactory : IDatabaseFactory
    {

        readonly DbProviderFactory _internalFactory;


        /// <summary>
        /// Creates a new instance of <see cref="DatabaseFactory"/>.
        /// </summary>
        /// <param name="providerFactory"></param>
        public DatabaseFactory(DbProviderFactory providerFactory)
        {
            _internalFactory = providerFactory;
        }


        /// <summary>
        /// Creates a new ADO.NET database connection using the provided connection string.
        /// </summary>
        public IDbConnection CreateDbConnection(string connectionString)
        {
            var conn = _internalFactory.CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

    }
}
