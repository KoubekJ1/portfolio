using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicplayer.dataobjects
{
    /// <summary>
    /// Data objects are object represantations from a database or other data sources
    /// </summary>
    public interface IDataObject
    {
        /// <summary>
        /// Retrieves the ID of the data object
        /// </summary>
        /// <returns>id</returns>
        public int? GetID();
    }
}
