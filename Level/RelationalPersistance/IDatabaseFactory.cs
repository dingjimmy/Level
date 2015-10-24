using System.Data;

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
}
