using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contoso.Model.Common;

namespace Contoso.Model
{
    public class Course : AuditableEntity
    {
        public Course()
        {
            CreatedDate = DateTime.Now;
        }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentId { get; set; }

        public  Department Department { get; set; }
        public  ICollection<Enrollment> Enrollments { get; set; }
        public  ICollection<Instructor> Instructors { get; set; }
    }
}