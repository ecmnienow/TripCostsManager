using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCostsManager.Domain.Entities.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime Creation { get; set; }
        DateTime? Modification { get; set; }
    }
}