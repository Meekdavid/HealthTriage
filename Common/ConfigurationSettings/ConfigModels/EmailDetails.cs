using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConfigurationSettings.ConfigModels
{
    public class EmailDetails
    {
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //public EmailDetails()
        //{
        //    UserName =  AESHelper.Decrypt(UserName) ?? string.Empty;
        //    Password =  AESHelper.Decrypt(Password) ?? string.Empty;
        //}
    }

}
