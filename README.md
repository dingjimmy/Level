Level
=====

An ADO.Net based data persistance framework structured around the Active Record pattern.


### Examples

##### Implement a Model entity as an ActiveRecord

```csharp
class Person : ActiveRecord<int>
{
   public string FirstName { get; set; }
   public string FamilyName { get; set; }
   public DateTime BornOn { get; set; }
   
   protected void Create() { ... }
   protected void Update() { ... }
   protected void Delete() { ... }
   public static Person Retrieve(int key) {... }
   
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
var person = Person.Retrieve(653987);
```

##### Querying the database for matching records...

```csharp
var people = Database.Where(new SearchByNameQuery() 
   { 
      FirstName == "Robert", 
      FamilyName == "Webb"
   });
   
var people = Database.First(new SearchByNameQuery() 
   { 
      FamilyName == "Webb"
   });
   
var havePeopleCalledRobert = Database.Any(new SearchByNameQuery() { FirstName == "Robert" });

var noOfPeopleCalledRobert = Database.Count(new SearchByNameQuery() { FirstName == "Robert" });
```

##### Creating a Table...

```csharp
Database.CreateTable<Person>();
```

##### Dropping a Table...
```csharp
Database.DropTable<Person>();
```


