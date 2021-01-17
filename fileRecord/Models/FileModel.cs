using System;
using System.ComponentModel.DataAnnotations;

namespace fileRecord.Models
{
    public class FileModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Filename { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
