using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace fileRecord.DTO
{
    public class FileUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Filename { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
