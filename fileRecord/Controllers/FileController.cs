using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using fileRecord.DTO;
using fileRecord.Models;
using fileRecord.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace fileRecord.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFile _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public FileController(IFile repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        /**
         * Get All Files
         * @verb "GET"
         * @endPoint "api/files"
         * **/
        [HttpGet]
        [Route("api/files")]
        public ActionResult<IEnumerable<FileReadDto>> GetAllFiles()
        {
            var allFiles = _repository.GetAllFiles();
            return Ok(_mapper.Map < IEnumerable < FileReadDto >> (allFiles));
        }

        /**
       * Get A File By ID
       * @verb "GET"
       * @endPoint "api/files/{id}"
       * **/
        [HttpGet("{id}", Name = "GetFileById")]
        [Route("api/files")]
        public ActionResult <FileReadDto> GetFileById(int Id)
        {
            var file = _repository.GetFileById(Id);
            if (file != null)
            {
                return Ok(_mapper.Map<FileReadDto>(file));
            }
            return NotFound();
        }

        [HttpPost()]
        [Route("api/files")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            string fileName;
            string path;
            try
            {
                fileName = file.FileName; 
                path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot//media");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                var fileModel = new FileModel { Filename = fileName, FilePath = "/media/"+ fileName };
                _repository.AddFile(fileModel);
                _repository.SaveChanges();
                var fileReadDto = _mapper.Map<FileReadDto>(fileModel);
                return CreatedAtRoute(nameof(GetFileById), new { Id = fileReadDto.Id }, fileReadDto);
            }
            catch
            {
                return BadRequest("Failed to save image");
            }
        }

        /**
         * Send Mail
         * @verb "POST"
         * @endPoint "api/files/sendMail"
         * **/
        [HttpPost()]
        [Route("api/files/sendMail")]
        public async Task<ActionResult> SendMail(MailDataDto data)
        {
            await DispatchMail(data);
            return Ok();
        }

        private static Task DispatchMail(MailDataDto data)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("juvitafrica@gmail.com");
                    message.To.Add(new MailAddress(data.Email));

                    if (!string.IsNullOrWhiteSpace(data.Bcc))
                    {
                        var splitted = data.Bcc.Split(',', ';');
                        foreach (var em in splitted)
                        {
                            message.Bcc.Add(new MailAddress(em));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(data.Cc))
                    {
                        var splitted = data.Cc.Split(',', ';');
                        foreach (var em in splitted)
                        {
                            message.CC.Add(new MailAddress(em));
                        }
                    }

                    message.Subject = data.Subject;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = data.EmailContent;

                    var files = data.files;
                    if (files != null && files.Any())
                    {
                        foreach (var file in files)
                        {
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot//media//{file}");
                            if (System.IO.File.Exists(filePath))
                            {
                                var fileByte = System.IO.File.ReadAllBytes(filePath);
                                message.Attachments.Add(new Attachment(new MemoryStream(fileByte), file));

                            }
                        }

                    }

                    smtp.Port = 2525;
                    smtp.Host = "smtp.mailtrap.io"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("USERNAME", "PASSWORD");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    
                    smtp.Send(message);
                }
                catch (Exception)
                {
                }
            });
            return task;
        }
    }
}
