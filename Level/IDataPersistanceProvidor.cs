using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Level
{
    /// <summary>
    /// When implemented, provides data manipulation and data definition functionality for a specific data persistance system.
    /// </summary>
    public interface IDataPersistanceProvidor
    {

        /// <summary>
        /// Initialize the provider. Initialization occours once at app startup; any subsequent calls are ingored.
        /// </summary>
        void Initialize();


        /// <summary>
        /// Creates a data persistance store, and uses it to persist all data sent to this providor. 
        /// If the specified store already exists, then it is used by this providor.
        /// </summary>
        void CreateStore();


        /// <summary>
        /// Drops a data persistance store.
        /// </summary>
        /// <param name="identifier">The unique identifier for the store you wish to drop.
        void DropStore(string identifier);


        /// <summary>
        /// Creates a persistance container for <see cref="T"/>.
        /// </summary>
        void CreateContainer<T>();


        /// <summary>
        /// Drop the persitance container for <see cref="T"/> 
        /// </summary>
        void DropContainer<T>();


        /// <summary>
        /// Inserts a <see cref="{T}"/> to the persistance store.
        /// </summary>
        void InsertRecord<T>(T record);


        /// <summary>
        /// Updates a <see cref="{T}"/> that already exists in the persistance store.
        /// </summary>
        void UpdateRecord<T>(T record);


        /// <summary>
        /// Deletes a <see cref="{T}"/> from the persistance store.
        /// </summary>
        void DeleteRecord<T>(T record);

    }
}
