using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.External.Enum
{
    public enum Status
    {
        Active,
        Inactive
    }

    public enum Title
    {
        Mr,
        Mrs,
        Miss,
        Ms,
        Sir,
        Madam
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum MarriedStatus
    {
        Single,
        Married,
        Separated
    }

    public enum DmsDatatype
    {
        General,
        Contact,
        Shipto,
        DMSSetting,
        Avatar,
        Merchandising,
        Location,
    }
}
