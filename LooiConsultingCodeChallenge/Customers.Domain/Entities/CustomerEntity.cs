using Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Customers.Domain.Entities
{
    [Index(propertyName: nameof(DNI), IsUnique = true)]
    [Index(propertyName: nameof(Email), IsUnique = true)]
    [Index(propertyName: nameof(Phone), IsUnique = true)]
    [Index(propertyName: nameof(Mobile), IsUnique = true)]
    [Table("Customers")]
    public sealed class CustomerEntity : BaseEntity
    {
        #region Properties
        [Required]
        [Column(TypeName = "varchar(30)")]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public required string DNI { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public required string Address { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string? Phone { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        public required string Mobile { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(254)")]
        public required string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public required string State { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public required string City { get; set; }
        #endregion
    }
}
