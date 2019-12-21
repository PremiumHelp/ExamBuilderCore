using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExamBuilder.Entities
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required, DataType(DataType.Text)]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
