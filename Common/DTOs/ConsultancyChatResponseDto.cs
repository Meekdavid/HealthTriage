using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ConsultancyChatResponseDto
    {
        public string ConsultancyChatId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }

}
