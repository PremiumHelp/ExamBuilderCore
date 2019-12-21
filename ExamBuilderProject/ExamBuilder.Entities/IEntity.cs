using System;
using System.ComponentModel.DataAnnotations;

namespace ExamBuilder.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        [DataType(DataType.DateTime)]
        DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        DateTime ModifiedDate { get; set; }
    }
}