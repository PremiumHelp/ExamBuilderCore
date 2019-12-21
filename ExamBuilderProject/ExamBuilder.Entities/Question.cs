using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamBuilder.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int Answer { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual List<Option> Options { get; set; }
    }
}