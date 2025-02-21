using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Core.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, StatusCode_Success, StatusMessage_Success)
        {

        }

        //public SuccessDataResult(string ResponseDescription) : base(default, StatusCode_Success, ResponseDescription)
        //{

        //}

    }
}
