using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Level.RelationalPersistance
{
    public class RelationalMapper : IDataMapper<TableMap>
    {

        private readonly ConcurrentDictionary<Type, TableMap> _Maps = new ConcurrentDictionary<Type, TableMap>();


        /// <summary>
        /// 
        /// </summary>
        public TableMap this[Type objectType]
        {
            get { return _Maps.ContainsKey(objectType) ? _Maps[objectType] : null; }
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

            // check if type already mapped
            if (_Maps.ContainsKey(t))
                throw new InvalidOperationException($"The type {t.Name} has already been mapped. You cannot map the same type twice.");


            // create maps
            var tblMap = MapTable(t);
            tblMap.ColumnMaps = MapColumns(t.GetProperties());
       

            // check that the primary key column has been discovered.
            if (!tblMap.ColumnMaps.Any(c => c.IsPrimaryKey))
                throw new InvalidOperationException("The primary key has not been specified.");


            // check we have exactly one primary key column; no more and no less.
            if (tblMap.ColumnMaps.Count(c => c.IsPrimaryKey) != 1)
                throw new InvalidOperationException("Compound primary keys are not currently supported.");


            // add map to cache
            _Maps.GetOrAdd(t, tblMap);

        }


        private TableMap MapTable(Type type)
        {
            return new TableMap() { Table = type.Name, Object = type.Name };
        }


        private ICollection<ColumnMap> MapColumns(IEnumerable<PropertyInfo> properties)
        {
            var maps = new List<ColumnMap>();

            foreach (var p in properties)
            {
                if (p.CanRead && p.CanWrite)
                {
                    var colMap = new ColumnMap();

                    colMap.ColumnName = p.Name;
                    colMap.IsPrimaryKey = p.Name.ToUpper() == "ID";
                    colMap.ColumnType = AdoDataType(p.PropertyType);
                    colMap.ColumnSize = AdoDataSize(p.PropertyType);
                    colMap.PropertyName = p.Name;
                    colMap.PropertyType = p.PropertyType;
                    colMap.AllowNull = !p.PropertyType.IsValueType && !colMap.IsPrimaryKey; // || p.PropertyType.IsAssignableFrom(typeof(Nullable));
                    
                    maps.Add(colMap);
                }
            }

            return maps;

        }


        /// <summary>
        /// Maps primitive CLR types to ADO.NET database column types.
        /// </summary>
        private DbType AdoDataType(Type t)
        {
            if (t == typeof(Char))
            {
                return DbType.StringFixedLength;
            }
            else if (t == typeof(String))
            {
                return DbType.String;
            }
            else if(t == typeof(Boolean))
            {
                return DbType.Boolean;
            }
            else if (t == typeof(Byte))
            {
                return DbType.Byte;
            }
            else if( t== typeof(Int16))
            {
                return DbType.Int16;
            }
            else if (t== typeof(Int32))
            {
                return DbType.Int32;
            }
            else if (t== typeof(Int64))
            {
                return DbType.Int64;
            }
            else if (t == typeof(Single))
            {
                return DbType.Single;
            }
            else if (t == typeof(Double))
            {
                return DbType.Double;
            }
            else
            {
                throw new NotSupportedException(t.FullName);
            }
        }


        /// <summary>
        /// Gets the appropriate ADO.NET column size for the given CLR type. A null value indicates 
        /// that the persistance store's default size for the given type should be used.
        /// </summary>
        private int? AdoDataSize(Type t)
        {
            if (t == typeof(Char))
            {
                return 1;
            }
            else
            {
                return null;
            }
        }
    }
}
