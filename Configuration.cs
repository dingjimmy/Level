namespace Level
{

  public class Database
  {
    
    public static string Name { get; set; }
    
    public static string Server { get; set; }
    
    public static string ProviderName { get; set; }
    
    public static IDbProviderFactory Provider { get; set; }
    
    
    public static IDbConnection OpenConnection() 
    {
      
    }
    
    
    public static IDbTransaction StartTransaction()
    {
      
    }
    
  }
  
}
