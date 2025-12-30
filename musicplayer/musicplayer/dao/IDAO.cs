using musicplayer.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataObject = musicplayer.dataobjects.IDataObject;

namespace musicplayer.dao
{
    /// <summary>
    /// Interface used for accessing data objects in the specified manner
    /// </summary>
    /// <typeparam name="T">DataObject</typeparam>
    public interface IDAO<T> where T : IDataObject
    {
        /// <summary>
        /// Retrieves an instance based on the given ID
        /// </summary>
        /// <param name="id">Instance ID</param>
        /// <returns>Instance</returns>
        public T? GetByID(int id);

        /// <summary>
        /// Retrives all instances of the type
        /// </summary>
        /// <returns>All instances</returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Uploads the instance into the database
        /// </summary>
        /// <param name="data">instance</param>
        /// <returns>instance ID</returns>
        public int? Upload(T data);

        /// <summary>
        /// Removes the instance from the database
        /// </summary>
        /// <param name="id">instance ID</param>
        public void Remove(int id);
    }
}
