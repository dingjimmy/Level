Level
=====

An ADO.Net based data persistance framework structured around the Active Record pattern.


### Examples

##### Implement a Model entity as an ActiveRecord

```csharp
class Person : ActiveRecord
{

   public string FirstName { get; set; }
   public string FamilyName { get; set; }
   public DateTime BornOn { get; set; }
   
   protected void Create() { ... }
   protected void Update() { ... }
   protected void Delete() { ... }
   public static Person Retrieve(key) {... }
   
}
```

##### Creating a new record...

```csharp
var person = new Person() 
{ 
   FirstName = "Joeseph", 
   FamilyName = "Bloggs", 
   BornOn = "1986-03-21" 
};

person.Save();
```

##### Updating a record...

```csharp
person.FamilyName = "Dreamcoat";
person.Save()
```

##### Retrieving an existing record..

```csharp
var person = ActiveRecord.RetrieveFirst<Person>( p => p.Firstname == "Joeseph" && 
                                                 p.FamilyName = "Bloggs");
```

##### Creating a Table...

```csharp
ActiveRecord.CreateTable<Person>();
```

##### Dropping a Table...
```csharp
ActiveRecord.DropTable<Person>();
```


