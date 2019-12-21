using System;
using System.Collections.Generic;
using System.Text;

namespace ExamBuilder.Business
{
    public class ProcessResult
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
