using System;

namespace Level
{
    public interface IDataMapper<T>
    {


        void Map<TObject>();


        void Map(Type t);


        T this[Type objectType] { get; }

    }
}