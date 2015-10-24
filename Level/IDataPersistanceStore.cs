
namespace Level
{
    public interface IDataPersistanceStore<T>
    {

        IDataMapper<T> DataMapper { get; set; }


        TObject Create<TObject>(TObject data);


        TObject Retrieve<TObject, TKey>(TKey key);


        void Update<TObject>(TObject data);


        void Delete<TObject, TKey>(TKey key);

    }
}