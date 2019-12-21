using ExamBuilder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamBuilder.Presentation.Models
{
    public class ExamViewModel
    {
        public Question Question { get; set; }
        public Exam Exam { get; set; }
        public Option Option1 { get; set; }
        public Option Option2 { get; set; }
        public Option Option3 { get; set; }
        public Option Option4 { get; set; }
    }
}
