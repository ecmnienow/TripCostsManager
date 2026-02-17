using System.ComponentModel;

namespace TripCostsManager.Domain.Entities.Enums
{
    public enum EPurpose
    {
        [Description("Souvenier")]
        Souvenier = 0,
        [Description("Present")]
        Present = 1,
        [Description("Transportation")]
        Transportation = 2,
        [Description("Entertainment")]
        Entertainment = 3,
        [Description("Purshase request")]
        PurshaseRequest = 4,
        [Description("Product purshase")]
        ProductPurshase = 5,
        [Description("Local consuming")]
        LocalConsuming = 6
    }
}
