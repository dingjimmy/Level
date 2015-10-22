public interface IActiveRecord
{
   RecordState DirtyState { get; set; }
}


public static class ActiveRecordExtensions
{
    public static bool Save(this IActiveRecord record)
    {
        return true;
    }
}
