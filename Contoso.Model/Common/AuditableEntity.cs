using System;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Model.Common
{
    public class AuditableEntity : Entity
    {
        [ScaffoldColumn(false)]
        public DateTime? CreatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }
    }
}