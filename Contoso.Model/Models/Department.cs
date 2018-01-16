using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contoso.Model.Common;

namespace Contoso.Model
{
    public class Department : AuditableEntity
    {
        public Department()
        {
            CreatedDate = DateTime.Now;
        }

        [MaxLength(150)]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }

        public  Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}