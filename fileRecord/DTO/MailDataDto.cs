using System;
using System.Collections.Generic;

namespace fileRecord.DTO
{
    public class MailDataDto
    {
        public List<string> files { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string EmailContent { get; set; }
    }
}
