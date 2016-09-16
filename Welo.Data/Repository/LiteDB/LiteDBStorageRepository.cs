using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using LiteDB;

namespace Welo.Data.Repository.LiteDB
{
    public class LiteDBStorageRepository<TFile> where TFile : class, IStorageFile, new()
    {
        private LiteFileStorage _fileStorage;

        public LiteDBStorageRepository(ILiteDBContext dbContext)
        {
            Contract.Requires(dbContext != null);

            _fileStorage = dbContext.Database.FileStorage;
        }

        public TFile Add(TFile file)
        {
            _fileStorage.Upload(file.Id, new MemoryStream(file.Content));
            return file;
        }

        public TFile Get(String idOrName)
        {
            LiteFileInfo fileInfo = _fileStorage.FindById(idOrName);

            if (fileInfo == null)
            {
                return null;
            }

            return fileInfo.ToStorageFile<TFile>();
        }

        public IEnumerable<TFile> GetAll()
        {
            foreach (LiteFileInfo fileInfo in _fileStorage.FindAll())
            {
                yield return fileInfo.ToStorageFile<TFile>();
            }
        }

        public bool Remove(String id)
        {
            return _fileStorage.Delete(id);
        }

        public bool Remove(TFile file)
        {
            return Remove(file.Id);
        }

        public TFile Update(TFile file)
        {
            return Add(file);
        }
    }
}
