using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Core.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string ResponseDescription) : base(StatusCode_Success, ResponseDescription)
        {

        }

        public SuccessResult() : base(StatusCode_Success)
        {

        }
    }
}
