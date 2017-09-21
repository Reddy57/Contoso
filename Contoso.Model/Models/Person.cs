using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contoso.Model.Common;
using Contoso.Model.Models;

namespace Contoso.Model
{
    /*
     * Below are three different approaches to represent an inheritance hierarchy in Code-First:

    Table per Hierarchy (TPH): This approach suggests one table for the entire class inheritance hierarchy. 
    Table includes discriminator column which distinguishes between inheritance classes. 
    This is a default inheritance mapping strategy in Entity Framework.
    TPH Violates the Third Normal Form
    TPH Requires Properties in SubClasses to be Nullable in the Database
    The disadvantages of the TPH strategy may be too serious for your design—after all, denormalized schemas can become a major burden in the long run.
    Your DBA may not like it at all. 
   
    Table per Type (TPT): This approach suggests a separate table for each domain class.
    Even though this mapping strategy is deceptively simple, the experience shows that performance can be unacceptable for complex class hierarchies
    because queries always require a join across many tables. In addition, this mapping strategy is more difficult to implement by hand— even ad-hoc
    reporting is more complex. This is an important consideration if you plan to use handwritten SQL in your application 
    (For ad hoc reporting, database views provide a way to offset the complexity of the TPT strategy.
    A view may be used to transform the table-per-type model into the much simpler table-per-hierarchy model.)



    
    Table per Concrete class (TPC): This approach suggests one table for one concrete class,
    but not for the abstract class. So, if you inherit the abstract class in multiple concrete classes, 
    then the properties of the abstract class will be part of each table of the concrete class.

     */


    public abstract class Person : AuditableEntity
    {
        [Required]
        [StringLength(50)]
        [MaxLength(150)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(150)]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }

        public string FullName => LastName + ", " + FirstName;

        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Email")]
        [MaxLength(150)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(150)]
        public string Phone { get; set; }

        [MaxLength(150)]
        public string AddressLine1 { get; set; }

        [MaxLength(150)]
        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        [DisplayName("Unit")]
        public string UnitOrApartmentNumber { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [DisplayName("Zip Code")]
        [MaxLength(20)]
        public string ZipCode { get; set; }

        [DisplayName("Password")]
        [MaxLength(20)]
        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public string Salt { get; set; }

        [ScaffoldColumn(false)]
        public bool IsLocked { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastLockedDateTime { get; set; }

        [ScaffoldColumn(false)]
        public int? FailedAttempts { get; set; }


        public virtual ICollection<Role> Roles { get; set; }
    }
}