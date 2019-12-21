using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamBuilder.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual IEnumerable<Option> Options { get; set; }
    }
}