using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCostsManager.Domain.Entities.Entities
{
    [Table("Images")]
    public class ImageEntity : BaseEntity
    {
        #region Attributes and Properties

        public int RecordId { get; set; }

        //[Required]
        [ForeignKey("RecordId")]
        public virtual RecordEntity Record { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Hash { get; set; }

        #endregion
    }
}
