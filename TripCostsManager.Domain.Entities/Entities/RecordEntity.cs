using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Domain.Entities.Entities
{
    [Table("Records")]
    public class RecordEntity : BaseEntity
    {
        #region Attributes and Properties

        [Required]
        [MaxLength(200)]
        public required string MarketName { get; set; }
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }
        [MaxLength(4000)]
        public required string Description { get; set; }
        public EPurpose Purpose { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }

        public int ItemTypeId { get; set; }
        [ForeignKey("ItemTypeId")]
        public virtual ItemTypeEntity ItemType { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual CurrencyEntity Currency { get; set; }
        public virtual ICollection<ImageEntity> Images { get; set; }

        #endregion
    }
}
