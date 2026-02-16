using System.ComponentModel;

namespace TripCostsManager.Services
{
    public enum EButtonType
    {
        [Description("primary")]
        Primary,
        [Description("sencondary")]
        Sencondary,
        [Description("default")]
        Default,
        [Description("active")]
        Active,
        [Description("error")]
        Error
    }
}
