using System;
using System.ComponentModel.DataAnnotations;

namespace Contoso.API.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }
        
    }
}