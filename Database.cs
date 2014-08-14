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
    
    
      
    public static IEnumerable<T> Where<T>(IQuery<T> queryToExecute)
    {
      
    }
    
    
    
    public static IEnumerable<T> First<T>(IQuery<T> queryToExecute)
    {
      
    }
    
    
    
    public static bool Any<T>(IQuery<T> queryToExecute)
    {
      
    }
  
}
