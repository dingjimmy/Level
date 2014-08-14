public class ActiveRecord<TKey> : IActiveRecord<TKey>
{
  
  private TKey _Key
  public TKey Key
  {
    get { return _Key; }
    private set { _Key = value; }
  }

  public RecordState State { get; set; }


  private bool Save() 
  {

    try
    {

      switch(State)
      {
        case New: 
          Create();
          return true;

        case Dirty:
          Update();
          return true;

        case Rubbish:
          Delete();
          return true;

        default:
          return true;
      }

    }
    catch(CreateRecordException ex)
    {
      //...
      return false;
    }
    catch(UpdateRecordException ex)
    {
      //...
      return false;
    }
    catch(DeleteRecordException ex)
    {
      //...
      return false;
    }
    catch(Exception ex)
    {
      //...
      return false;
    }

  }
  

  protected abstract void Create();

  protected abstract void Update();

  protected abstract void Delete();

}
