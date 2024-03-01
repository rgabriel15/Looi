using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Common.Domain.Entities
{
    public abstract class BaseEntity
    {
        #region Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = nameof(SqlDbType.Int))]
        public uint Id { get; set; }
        #endregion
    }
}
