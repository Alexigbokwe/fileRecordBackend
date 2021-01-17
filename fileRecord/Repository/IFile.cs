using System;
using System.Collections.Generic;
using fileRecord.Models;

namespace fileRecord.Repository
{
    public interface IFile
    {
        bool SaveChanges();
        IEnumerable<FileModel> GetAllFiles();
        FileModel GetFileById(int Id);
        void AddFile(FileModel userfile);
        void UpdateFile(FileModel userfile);
        void DeleteFile(FileModel userfile);
    }
}
