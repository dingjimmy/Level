Level
=====

A data persistance framework based upon the Active Record pattern.


### Examples

##### 1: Creating a new record...

```csharp
var person = new PersonRecord() 
{ 
   FirstName = "Joeseph", 
   FamilyName = "Bloggs", 
   BornOn = "1986-03-21" 
};

person.Save();
```


##### 2: Retrieving an existing record..

```csharp
var person = ActiveRecord.RetrieveFirst( p=> p.Firstname == "Joeseph" && 
                                             p.FamilyName = "Bloggs");
```

##### 3: Updating a record...

```csharp
person.FamilyName = "Dreamcoat";
person.Update()
```

### Public Interface

```csharp
public interface IActiveRecord
{
   RecordState State { get; set; }
   bool Save();
}

public enum RecordState
{
    New,
    Clean,
    Dirty,
    Rubbish
}
```

### Base Class

```csharp
public partial class ActiveRecord
{
  
  private bool Save() 
  {
  
    try
    {
    
      switch(State)
      {
        case New: 
          Create();
          break;
        
        case Dirty:
          Update();
          break;
        
        case Rubbish:
          Delete();
          break;
          
        default:
          break;
      }
      
    }
    catch(CreateRecordException ex)
    {
      ...
    }
    catch(UpdateRecordException ex)
    {
      ...
    }
    catch(DeleteRecordException ex)
    {
      ...
    }
    catch(Exception ex)
    {
      ...
    }
  
  }
  
  protected abstract void Create() {...}
    
  protected abstract void Update() {...}
  
  protected abstract void Delete() {...}
    

}
```

### Static Methods & Properites

```csharp
public partial class ActiveRecord
{
  public static string ConnectionString { get; set; }
  public static IDbConnection Connection { get; set; }

  public static bool Any<TRecord>(expression) { };
  public static IActiveRecord RetrieveFirst<TRecord>(expression) { };
  public static ICollection<IActiveRecord> RetrieveAll<TRecord> (expression) {};
}
```
    
         
      
