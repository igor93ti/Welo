using System;

namespace Welo.Data.Repository.LiteDB
{
    /// <summary>
    /// Storage file
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IStorageFile
    {
        /// <summary>
        /// Id and Name
        /// </summary>
        String Id { get; set; }

        /// <summary>
        /// File contents
        /// </summary>
        Byte[ ] Content { get; set; }
    }

}