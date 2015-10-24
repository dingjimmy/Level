using System;
using System.Collections.Concurrent;
using System.Data;

namespace Level.RelationalPersistance
{
    /// <summary>
    /// Provides data manipulation and data definition functionality for a relational databases.
    /// </summary>
    public class RelationalPersistanceStore : IDataPersistanceStore<TableMap>
    {

        public string ConnectionString { get; }


        public IDatabaseFactory DatabaseFactory { get; }


        public IDataMapper<TableMap> DataMapper { get; set; }


        /// <summary>
        /// Creates a new instance of <see cref="RelationalPersistanceStore"/>.
        /// </summary>
        public RelationalPersistanceStore(IDatabaseFactory dbFactory, string connectionString)
        {

            if (dbFactory == null) throw new ArgumentNullException(nameof(dbFactory));
            if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            DatabaseFactory = dbFactory;
            ConnectionString = connectionString;

            DataMapper = new RelationalMapper();

        }


        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {

            // get all loaded assemblies
            var arType = typeof(IActiveRecord);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();


            // find all classes in all assemblies that implement IActiveRecord, and create an object-relational map for them.
            foreach (var asm in assemblies)
            {
                var types = asm.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsAssignableFrom(arType))
                    {
                        DataMapper.Map(t);
                    }
                }
            }

        }


        public TObject Create<TObject>(TObject data)
        {
            throw new NotImplementedException();
        }

        public TObject Retrieve<TObject, TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Update<TObject>(TObject data)
        {
            throw new NotImplementedException();
        }

        public void Delete<TObject, TKey>(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}
