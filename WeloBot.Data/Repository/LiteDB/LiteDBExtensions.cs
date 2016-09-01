using System.IO;
using LiteDB;

namespace WeloBot.Data.Repository.LiteDB
{
    public static class LiteDBExtensions
    {
        public static TFile ToStorageFile<TFile>( this LiteFileInfo fileInfo ) where TFile : class, IStorageFile, new()
        {
            TFile file = new TFile
            {
                Id = fileInfo.Id
            };

            // Read
            using( LiteFileStream fs = fileInfo.OpenRead() )
            using( MemoryStream ms = new MemoryStream() )
            {
                fs.CopyTo( ms );

                file.Content = ms.ToArray();
            }

            return file;
        }
    }
}
