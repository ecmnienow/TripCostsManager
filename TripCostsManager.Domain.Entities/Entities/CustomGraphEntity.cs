using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Domain.Entities.Entities
{
    [Table("CustomGraphs")]
    public class CustomGraphEntity : BaseEntity
    {
        #region Attributes and Properties

        [Required]
        [MaxLength(50)]
        public required string Title { get; set; }

        [Required]
        public required EGraphType Type { get; set; }

        [Required]
        public required string ColumnNames { get; set; }

        public required string Script { get; set; }

        #endregion
    }
}
