using System;
using Level.RelationalPersistance;

namespace Level.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = @"server=(localdb)\mssqllocaldb;database=level-test";
            var mapper = new RelationalMapper();
            var provider = new SqlServerPersistanceProvider(connStr, mapper);


            mapper.Map<Foo>();
            mapper.Map<Bar>();

            var fooMap = mapper[typeof(Foo)];
            var barMap = mapper[typeof(Bar)];

            WriteMap(fooMap);
            WriteMap(barMap);

            Console.Read();

            if (!provider.TableExists<Foo>())
            {
                Console.WriteLine("Foo table already exists.");
                Console.Write("Deleting Foo table...");

                provider.DropTable<Foo>();

                Console.WriteLine("done.");
            }


            try
            {
                provider.CreateTable<Foo>();
                Console.WriteLine("Foo table created...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Foo table creation failed... {ex.Message}");
            }


            try
            {
                provider.CreateTable<Bar>();
                Console.WriteLine("Bar table created...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bar table creation failed... {ex.Message}");
            }



            provider.Insert(new Foo() { ID = 1001, Name = "Mr Foo", Age = 234, IsHuman = true, Location = 192.456f });
            provider.Insert(new Foo() { ID = 1001, Name = "Mr Weener", Age = 24, IsHuman = false, Location = 987.009208f });


            

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
