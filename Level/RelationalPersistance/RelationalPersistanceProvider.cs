using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Level.RelationalPersistance
{
    /// <summary>
    /// Provides data manipulation and data definition functionality for a relational databases.
    /// </summary>
    public class RelationalPersistanceProvider : IDataPersistanceProvidor
    {

        public string ConnectionString { get; }


        public IDatabaseFactory DatabaseFactory { get; }
        

        public ConcurrentDictionary<Type, TableMap> ObjectRelationalMap { get; }


        /// <summary>
        /// Creates a new instance of <see cref="RelationalPersistanceProvider"/>.
        /// </summary>
        public RelationalPersistanceProvider(IDatabaseFactory dbFactory, string connectionString)
        {

            if (dbFactory == null) throw new ArgumentNullException(nameof(dbFactory));
            if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            DatabaseFactory = dbFactory;
            ConnectionString = connectionString;

            ObjectRelationalMap = new ConcurrentDictionary<Type, TableMap>();

        }


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            // find all classes that implement IActiveRecord
            var arType = typeof(IActiveRecord);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var asm in assemblies)
            {
                var types = asm.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsAssignableFrom(arType))
                    {
                        Map(t);
                    }
                }
            }
        }


        /// <summary>
        /// Maps the specified type to a relational database table.
        /// </summary>
        public void Map<T>()
        {
            Map(typeof(T));          
        }


        /// <summary>
        /// Maps the specified type to a relational database table.
        /// </summary>
        public void Map(Type t)
        {

            var tblMap = new TableMap() { Table = t.Name, Object = t.Name };

            var props = t.GetProperties();

            foreach (var p in props)
            {
                if (p.CanRead && p.CanWrite)
                {
                    var colMap = new ColumnMap();
                    colMap.Column = p.Name;
                    colMap.Property = p.Name;
                    colMap.IsPrimaryKey = p.Name.ToUpper() == "ID";
                }
            }

            // check that the primary key column has been discovered.
            if (!tblMap.ColumnMaps.Any(c=> c.IsPrimaryKey))
            {
                throw new InvalidOperationException("A primary key has not been specified.");
            }

            // check we have exactly one primary key column; no more and no less.
            if (tblMap.ColumnMaps.Count(c=> c.IsPrimaryKey) != 1)
            {
                throw new InvalidOperationException("Comound primary keys are not currently supported.");
            }

            ObjectRelationalMap.GetOrAdd(t, tblMap);

        }

        /// <summary>
        /// Checks to see if the server and database specified in the connection string are accessable.
        /// </summary>
        public virtual bool StoreExists()
        {
            using (var conn = DatabaseFactory.CreateDbConnection(this.ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Creates a new database.
        /// </summary>
        public virtual void CreateStore()
        {
            using (var conn = DatabaseFactory.CreateDbConnection(this.ConnectionString))
            {
                
            }          
        }


        /// <summary>
        /// Drops the existing database.
        /// </summary>
        /// <param name="identifier"></param>
        public virtual void DropStore(string identifier)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Checks if a table already exists for the given type.
        /// </summary>
        public bool ContainerExists<T>()
        {
            var t = typeof(T);

            if (!ObjectRelationalMap.ContainsKey(t)) throw new InvalidOperationException($"The type '{t.Name}' is not currently mapped to a table. A type must be mapped before any database operations can be performed with it.");

            var map = ObjectRelationalMap[typeof(T)];

            using (var conn = DatabaseFactory.CreateDbConnection(this.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT 1 FROM [{map.Table}] WHERE 1 = 0";
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }


        /// <summary>
        /// Creates a table.
        /// </summary>
        public void CreateContainer<T>()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Drops an existing table.
        /// </summary>
        public void DropContainer<T>()
        {
            throw new NotImplementedException();
        }

       
        /// <summary>
        /// Inserts a record of the given type into its associated table.
        /// </summary>
        public void InsertRecord<T>(T record)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Updates an existing record with new values.
        /// </summary>
        public void UpdateRecord<T>(T record)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes an existing record from its associated table.
        /// </summary>
        public void DeleteRecord<T>(T record)
        {
            throw new NotImplementedException();
        }

    }
}
