using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class SuccessfulLoginDto
    {
        public Data data { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
    }

    public class Data
    {
        public string accessToken { get; set; }
        public DateTime expiration { get; set; }
        public string refreshToken { get; set; }
    }

}
