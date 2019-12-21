using System;
using System.ComponentModel.DataAnnotations;

namespace ExamBuilder.Entities
{
    public class Option : IEntity
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required, DataType(DataType.Text)]
        public string Text { get; set; }

        public virtual Question Question { get; set; }
    }
}