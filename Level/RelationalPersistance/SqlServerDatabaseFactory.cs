using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;

namespace Level.RelationalPersistance
{

    /// <summary>
    /// 
    /// </summary>
    public class SqlServerDatabaseFactory : IDatabaseFactory
    {

        readonly DbProviderFactory _internalFactory;


        /// <summary>
        /// Creates a new instance of <see cref="SqlServerDatabaseFactory"/>.
        /// </summary>
        /// <param name="providerFactory"></param>
        public SqlServerDatabaseFactory()
        {
            _internalFactory = SqlClientFactory.Instance;
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


        /// <summary>
        /// Checks if a table already exists for the given table mapping.
        /// </summary>
        public bool TableExists(TableMap map, string connectionString)
        {
            return ExecuteCommand($"SELECT 1 FROM[{ map.Table}] WHERE 1 = 0", connectionString);
        }


        /// <summary>
        /// 
        /// </summary>
        public bool CreateTable(TableMap map, string connectionString)
        {

            // build sql command
            var sb = new StringBuilder($"CREATE TABLE [{ map.Table }] ");
            sb.AppendLine();
            sb.AppendLine("(");

            var lineCount = 0;
            foreach (var col in map.ColumnMaps)
            {
                lineCount++;
                sb.Append($"  [{col.ColumnName}] {SqlServerDataType(col.ColumnType, col.ColumnSize)} {Nullable(col)} {PrimaryKey(col)}");

                if (lineCount != map.ColumnMaps.Count)
                    sb.AppendLine(",");
                else
                    sb.AppendLine();

            }
            sb.Append(")");


            // execute sql command
            return ExecuteCommand(sb.ToString(), connectionString);

        }

        
        /// <summary>
        /// 
        /// </summary>
        public void DropTable(TableMap map, string connectionString)
        {

        }


        private bool ExecuteCommand(string sqlStr, string conStr)
        {
            using (var conn = CreateDbConnection(conStr))
            {
                try
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlStr;
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        private string PrimaryKey(ColumnMap map)
        {
            if (map.IsPrimaryKey)
                return "PRIMARY KEY";
            else
                return string.Empty;
        }


        private string Nullable(ColumnMap map)
        {
            if (map.AllowNull)
                return "NULL";
            else
                return "NOT NULL";
        }


        /// <summary>
        /// Maps Ado.net column data types to sqlserver column data types.
        /// </summary>
        private string SqlServerDataType(DbType adoDataType, int? size)
        {
            switch (adoDataType)
            {
                case DbType.AnsiString:
                    var vsz = size.HasValue == true ? size.Value.ToString() : "MAX";
                    return $"VARCHAR({vsz})";

                case DbType.Binary:
                    var vbsz = size.HasValue == true ? size.Value.ToString() : "MAX";
                    return $"VARBINARY({vbsz})";

                case DbType.Byte:
                    return "TINYINT";

                case DbType.Boolean:
                    return "BIT";

                case DbType.Currency:
                    return "MONEY";

                case DbType.Date:
                    return "DATE";

                case DbType.DateTime:
                    return "DATETIME";

                case DbType.Decimal:
                    throw new NotSupportedException(nameof(DbType.Decimal));

                case DbType.Double:
                    return "FLOAT";

                case DbType.Guid:
                    throw new NotSupportedException(nameof(DbType.Guid));

                case DbType.Int16:
                    return "SMALLINT";

                case DbType.Int32:
                    return "INT";

                case DbType.Int64:
                    return "BIGINT";

                case DbType.Object:
                    throw new NotSupportedException(nameof(DbType.Object));

                case DbType.SByte:
                    throw new NotSupportedException(nameof(DbType.SByte));

                case DbType.Single:
                    return "REAL";

                case DbType.String:
                    var nvsz = size.HasValue == true ? size.Value.ToString() : "MAX";
                    return $"NVARCHAR({nvsz})";

                case DbType.Time:
                    return "TIME";

                case DbType.UInt16:
                    throw new NotSupportedException(nameof(DbType.UInt16));

                case DbType.UInt32:
                    throw new NotSupportedException(nameof(DbType.UInt32));

                case DbType.UInt64:
                    throw new NotSupportedException(nameof(DbType.UInt64));

                case DbType.VarNumeric:
                    throw new NotSupportedException(nameof(DbType.VarNumeric));

                case DbType.AnsiStringFixedLength:
                    var csz = size.HasValue == true ? size.Value.ToString() : "1";
                    return $"CHAR({csz})"; ;

                case DbType.StringFixedLength:
                    var vcsz = size.HasValue == true ? size.Value.ToString() : "1";
                    return $"NCHAR({vcsz})";

                case DbType.Xml:
                    return "XML";

                case DbType.DateTime2:
                    return "DATETIME2";

                case DbType.DateTimeOffset:
                    return "DATETIMEOFFSET";

                default:
                    throw new NotSupportedException("INVALID DATA TYPE");
            }
        }

    }
}
