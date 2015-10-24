using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Level.RelationalPersistance
{

    /// <summary>
    /// Maps a CLR object to a relational databsae table.
    /// </summary>
    public class TableMap
    {

        /// <summary>
        /// The name of the relational database table.
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The name of the CLR object.
        /// </summary>
        public string Object { get; set; }


        /// <summary>
        /// A collection of Property->Column maps.
        /// </summary>
        public ICollection<ColumnMap> ColumnMaps { get; set; }


        /// <summary>
        /// Creates a new instance of <see cref="TableMap"/>
        /// </summary>
        public TableMap()
        {
            ColumnMaps = new List<ColumnMap>();
        }

    }


    /// <summary>
    /// Maps a CLR object property to a relational database table column.
    /// </summary>
    public class ColumnMap
    {

        /// <summary>
        /// Specifies if this column map is for a primary key column.
        /// </summary>
        public bool IsPrimaryKey { get; set; }


        /// <summary>
        /// The name of the column to map.
        /// </summary>
        public string ColumnName { get; set; }


        public System.Data.DbType ColumnType { get; set; }


        /// <summary>
        /// The size of the column to map.
        /// </summary>
        public int ColumnSize { get; set; }


        /// <summary>
        /// The name of the clr property to map
        /// </summary>
        public string PropertyName { get; set; }


        /// <summary>
        /// The type of the clr property to map.
        /// </summary>
        public Type PropertyType { get; set; }

    }
}
