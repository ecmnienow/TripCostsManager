using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCostsManager.Domain.Entities.Interfaces;

namespace TripCostsManager.Domain.Entities.Entities
{
    public class BaseEntity : IBaseEntity
    {
        #region Contructor

        public BaseEntity()
        {
            this.Creation = DateTime.Now;
        }

        #endregion

        #region IBaseEntity Members

        [Key]
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        public DateTime? Modification { get; set; }

        #endregion
    }
}
