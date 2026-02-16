using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TripCostsManager.Domain.Entities.Enums;
using TripCostsManager.Domain.Entities.Interfaces;

namespace TripCostsManager.Domain.Entities.Entities
{
    [Table("Tasks")]
    public class RecordEntity : BaseEntity
    {
        #region Attributes and Properties

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }
        [MaxLength(4000)]
        public required string Description { get; set; }
        public EType Type { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ICollection<ImageEntity> Images { get; set; }

        #endregion
    }
}
