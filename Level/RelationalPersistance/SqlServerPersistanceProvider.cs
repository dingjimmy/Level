using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Level.RelationalPersistance
{
    /// <summary>
    /// Provides the ability to persist and retrieve objects from a relational database.
    /// </summary>
    public class SqlServerPersistanceProvider : IDataPersistanceProvider
    {

        readonly DbProviderFactory _internalFactory;
        readonly IDataMapper<TableMap> _dataMapper;
        readonly string _connString;


        /// <summary>
        /// Creates a new instance of <see cref="SqlServerPersitanceProvider"/>.
        /// </summary>
        public SqlServerPersistanceProvider(string connectionString, IDataMapper<TableMap> dataMapper)
        {

            if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (dataMapper == null) throw new ArgumentNullException(nameof(dataMapper));

            _internalFactory = SqlClientFactory.Instance;
            _dataMapper = dataMapper;
            _connString = connectionString;

        }


        /// <summary>
        /// Checks if a table already exists for <see cref="{TObject}"/>.
        /// </summary>
        public bool TableExists<TObject>()
        {
            var map = _dataMapper[typeof(TObject)];

            try
            {
                ExecuteCommand($"SELECT 1 FROM[{ map.Table}] WHERE 1 = 0");
                return true;
            }
            catch (Exception ex)
            {
                throw new DataException($"Unable to confirm if table [{map.Table}] exists. See inner exception for more detail.", ex);
            }
            
        }


        /// <summary>
        /// Creates a table for the <see cref="{TObject}"/> class.
        /// </summary>
        public void CreateTable<TObject>()
        { 
            
            // get map
            var map = _dataMapper[typeof(TObject)];


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
            try
            {
                ExecuteCommand(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new DataException($"Unable to create table [{map.Table}].", ex);
            }
            
        }


        /// <summary>
        /// Drops the table for <see cref="{TObject}"/>, if it exists.
        /// </summary>
        public void DropTable<TObject>()
        {

            // get the map
            var map = _dataMapper[typeof(TObject)];

            // drop the table
            try
            {
                ExecuteCommand($"DROP TABLE [{map.Table}]");
            }
            catch (Exception ex)
            {
                throw new DataException($"Unable to drop table [{map.Table}].", ex);
            }

        }



        public TObject Insert<TObject>(TObject data)
        {
            throw new NotImplementedException();
        }

        public TObject Retrieve<TObject, TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public TObject Update<TObject>(TObject data)
        {
            throw new NotImplementedException();
        }

        public void Delete<TObject, TKey>(TKey key)
        {
            throw new NotImplementedException();
        }


#region Privates


        // Creates a new ADO.NET database connection.
        private IDbConnection CreateDbConnection()
        {
            var conn = _internalFactory.CreateConnection();
            conn.ConnectionString = _connString;
            return conn;
        }


        // Executes the given sql command.
        private void ExecuteCommand(string sqlCmd)
        {
            using (var conn = CreateDbConnection())
            {
                try
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlCmd;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new DataException($"Failed to execute sql command \"{sqlCmd}\". See inner exception for further details.", ex);
                }
            }
        }


        // Executes the given sql query and returns results as datareader.
        private IDataReader ExecuteQuery(string sqlQuery, IEnumerable<IDataParameter> parameters)
        {
            using (var conn = CreateDbConnection())
            {
                try
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlQuery;

                    return cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    throw new DataException($"Failed to execute sql query \"{sqlQuery}\". See inner exception for further details.", ex);
                }
            }
        }


        //
        private string PrimaryKey(ColumnMap map)
        {
            if (map.IsPrimaryKey)
                return "PRIMARY KEY";
            else
                return string.Empty;
        }


        //
        private string Nullable(ColumnMap map)
        {
            if (map.AllowNull)
                return "NULL";
            else
                return "NOT NULL";
        }


        // Maps Ado.net column data types to sqlserver column data types.
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


#endregion


    }
}
