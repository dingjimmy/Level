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
        public List<ColumnMap> ColumnMaps { get; set; }


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
        /// The name of the database table column.
        /// </summary>
        public string Column { get; set; }


        /// <summary>
        /// The name of the CLR object property.
        /// </summary>
        public string Property { get; set; }

    }
}
