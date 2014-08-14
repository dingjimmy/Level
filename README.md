Level
=====

An ADO.Net based data persistance framework structured around the Active Record pattern.


### Examples

##### 1: Creating a new record...

```csharp
var person = new Person() 
{ 
   FirstName = "Joeseph", 
   FamilyName = "Bloggs", 
   BornOn = "1986-03-21" 
};

person.Save();
```


##### 2: Retrieving an existing record..

```csharp
var person = ActiveRecord.RetrieveFirst<Person>( p => p.Firstname == "Joeseph" && 
                                                 p.FamilyName = "Bloggs");
```

##### 3: Updating a record...

```csharp
person.FamilyName = "Dreamcoat";
person.Update()
```

#### 4. Creating a Table...

```csharp
ActiveRecord.CreateTable<Person>();
```

#### 5. Dropping a Table...
```csharp
ActiveRecord.DropTable<Person>();
```


