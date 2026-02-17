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
    [Table("ItemTypes")]
    public class ItemTypeEntity : BaseEntity
    {
        #region Attributes and Properties

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        #endregion
    }
}
