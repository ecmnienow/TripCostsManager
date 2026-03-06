using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripCostsManager.Domain.Entities.Entities
{
    [Table("Currencies")]
    public class CurrencyEntity : BaseEntity
    {
        #region Attributes and Properties

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string CurrencySymbol { get; set; }

        [Required]
        public required decimal Value { get; set; }

        #endregion
    }
}
