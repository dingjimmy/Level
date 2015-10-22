using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Level
{

  public class PersistanceStore
  {
    
        public static string Name { get; set; }
    
        public static string Server { get; set; }
    
        public static string ProviderName { get; set; }
    
        public static DbProviderFactory Provider { get; set; }



        public static IDbConnection OpenConnection()
        {
            throw new NotImplementedException();
        }



        public static IDbTransaction StartTransaction()
        {
            throw new NotImplementedException();
        }



        public static IEnumerable<T> Where<T>(IQuery<T> queryToExecute)
        {
            throw new NotImplementedException();
        }
    
    
    
        public static IEnumerable<T> First<T>(IQuery<T> queryToExecute)
        {
            throw new NotImplementedException();
        }
    
    
    
        public static bool Any<T>(IQuery<T> queryToExecute)
        {
            throw new NotImplementedException();
        }
  
}
