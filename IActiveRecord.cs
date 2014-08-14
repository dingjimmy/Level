public interface IActiveRecord
{
   RecordState State { get; set; }
   bool Save();
}
