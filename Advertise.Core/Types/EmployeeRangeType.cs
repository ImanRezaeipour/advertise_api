using System.ComponentModel;

namespace Advertise.Core.Types
{
    public enum EmployeeRangeType
    {
        [Description("خویش فرما")]
        A = 0,

        [Description("1-10 نفر")]
        B = 1,

        [Description("11-50 نفر")]
        C = 2,

        [Description("51-200 نفر")]
        D = 3,

        [Description("201-500 نفر")]
        E = 4,

        [Description("501-1000 نفر")]
        F = 5,

        [Description("1001-5000 نفر")]
        G = 6,

        [Description("5001-10000 نفر")]
        H = 7,

        [Description("10001+ نفر")]
        I = 8,
    }
}