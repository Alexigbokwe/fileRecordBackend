using System;
namespace fileRecord.DTO
{
    public class FileReadDto
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public string FilePath { get; set; }

        public string CreatedAt { get; set; }
    }
}
