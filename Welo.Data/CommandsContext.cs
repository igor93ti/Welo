using System;
using System.IO;
using LiteDB;
using Welo.Data.Repository.LiteDB;

namespace Welo.Data
{
    [Serializable]
    public class CommandsContext : ILiteDBContext
    {
        private LiteDatabase db;

        public LiteDatabase Database
        {
            get
            {
                var appDomain = System.AppDomain.CurrentDomain;
                var basePath = appDomain.BaseDirectory;

                var pathDirectory = Path.Combine(basePath, "DataFile");
                if (!Directory.Exists(pathDirectory))
                    Directory.CreateDirectory(pathDirectory);

                var path = Path.Combine(pathDirectory, "CommandsContext");

                return db ?? (db = new LiteDatabase(path));
            }

            set { db = value; }
        }
    }
}