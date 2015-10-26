using System;
using Level.RelationalPersistance;

namespace Level.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = @"server=(localdb)\mssqllocaldb;database=level-test";
            var factory = new SqlServerDatabaseFactory();
            var store = new RelationalPersistanceStore(factory, connStr);


            store.DataMapper.Map<Foo>();
            store.DataMapper.Map<Bar>();

            var fooMap = store.DataMapper[typeof(Foo)];
            var barMap = store.DataMapper[typeof(Bar)];

            WriteMap(fooMap);
            Console.WriteLine();
            WriteMap(barMap);

            Console.Read();

            if (factory.CreateTable(fooMap, connStr))
                Console.WriteLine("Foo table created...");

            if(factory.CreateTable(barMap, connStr))
                Console.WriteLine("Bar table created...");

        }


        static void WriteMap(TableMap map)
        {
            Console.WriteLine(map.Table);
            foreach (var col in map.ColumnMaps)
            {
                Console.Write($"{col.ColumnName}, ");

                Console.Write($"{col.ColumnType}, ");

                if (col.ColumnSize.HasValue)
                    Console.Write($"{col.ColumnSize.Value}, ");
                else
                    Console.Write("default, ");

                if (col.AllowNull)
                    Console.Write("null, ");
                else
                    Console.Write("not null, ");

                if (col.IsPrimaryKey)
                    Console.Write("primary key, ");

                Console.WriteLine();
            }
        }


        class Foo
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }

            public bool IsHuman { get; set; }

            public float Location { get; set; }
        }

        class Bar
        {
            public string ID { get; set; }

            public long SchlongLength { get; set; }
        }
    }
}
