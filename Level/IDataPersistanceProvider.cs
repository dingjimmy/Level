
namespace Level
{
    public interface IDataPersistanceProvider
    {

        bool TableExists<TObject>();


        void CreateTable<TObject>();


        void DropTable<TObject>();


        TObject Insert<TObject>(TObject obj);


        TObject Retrieve<TObject, TKey>(TKey key);


        TObject Update<TObject>(TObject data);


        void Delete<TObject, TKey>(TKey key);

    }
}