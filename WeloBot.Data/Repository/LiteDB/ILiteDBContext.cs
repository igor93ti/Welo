﻿using LiteDB;

namespace WeloBot.Data.Repository.LiteDB
{
    /// <summary>
    /// Context of LiteDB
    /// </summary>
    public interface ILiteDBContext
    {
        /// <summary>
        /// Current used Database
        /// </summary>
        LiteDatabase Database { get; set; }
    }
}
