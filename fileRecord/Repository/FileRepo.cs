using System;
using System.Collections.Generic;
using System.Linq;
using fileRecord.Models;

namespace fileRecord.Repository
{
    public class FileRepo : IFile
    {
        private readonly Context.AppContext _context;

        public FileRepo(Context.AppContext context)
        {
            _context = context;
        }

        public void AddFile(FileModel userfile)
        {
            if (userfile == null)
            {
                throw new ArgumentNullException(nameof(userfile));
            }
            _context.Files.Add(userfile);
        }

        public void DeleteFile(FileModel userfile)
        {
            if (userfile == null)
            {
                throw new ArgumentNullException(nameof(userfile));
            }
            _context.Files.Remove(userfile);
        }

        public IEnumerable<FileModel> GetAllFiles()
        {
            return _context.Files.ToList();
        }

        public FileModel GetFileById(int Id)
        {
            return _context.Files.FirstOrDefault(p => p.Id == Id);
        }

        bool IFile.SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFile(FileModel userfile)
        {
            if (userfile == null)
            {
                throw new ArgumentNullException(nameof(userfile));
            }
            _context.Files.Update(userfile);
        }
    }
}
