using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SmartDealer.Models.Models
{
    public enum SalutationType
    {
        [EnumMember]
        [Description("")]
        None = 0,
        [EnumMember]
        [Description("Mr.")]
        Mr = 1,
        [EnumMember]
        [Description("Mrs.")]
        Mrs = 2,
        [EnumMember]
        [Description("Miss")]
        Miss = 3,
        [EnumMember]
        [Description("Ms.")]
        Ms = 4,
        [EnumMember]
        [Description("Prof.")]
        Prof = 5,
        [EnumMember]
        [Description("Dr.")]
        Dr = 6,
        [EnumMember]
        [Description("Gen.")]
        Gen = 7,
        [EnumMember]
        [Description("Rep.")]
        Rep = 8,
        [EnumMember]
        [Description("Sen.")]
        Sen = 9,
        [EnumMember]
        [Description("St.")]
        St = 10,
        [EnumMember]
        [Description("Rev.")]
        Rev = 11
    }
    public enum PhoneNumberType
    {
        [EnumMember]
        [Description("Unknown")]
        Unknown = 0,
        [EnumMember]
        [Description("Home")]
        Home = 1,
        [EnumMember]
        [Description("Work")]
        Work = 2,
        [EnumMember]
        [Description("Mobile")]
        Mobile = 3,
        [EnumMember]
        [Description("Home Fax")]
        HomeFax = 5,
        [EnumMember]
        [Description("Work Fax")]
        WorkFax = 6,
        [EnumMember]
        [Description("Mobile Fax")]
        MobileFax = 7,
        [EnumMember]
        [Description("Other")]
        Other = 10
    }
    public enum State
    {
        [EnumMember]
        [Description("")]
        None = 0,
        [EnumMember]
        [Description("Alabama")]
        AL = 1,
        [EnumMember]
        [Description("Alaska")]
        AK = 2,
        [EnumMember]
        [Description("Arizona")]
        AZ = 3,
        [EnumMember]
        [Description("Arkansas")]
        AR = 4,
        [EnumMember]
        [Description("California")]
        CA = 5,
        [EnumMember]
        [Description("Colorado")]
        CO = 6,
        [EnumMember]
        [Description("Connecticut")]
        CT = 7,
        [EnumMember]
        [Description("District of Columbia")]
        DC = 8,
        [EnumMember]
        [Description("Delaware")]
        DE = 9,
        [EnumMember]
        [Description("Florida")]
        FL = 10,
        [EnumMember]
        [Description("Georgia")]
        GA = 11,
        [EnumMember]
        [Description("Hawaii")]
        HI = 12,
        [EnumMember]
        [Description("Idaho")]
        ID = 13,
        [EnumMember]
        [Description("Illinois")]
        IL = 14,
        [EnumMember]
        [Description("Indiana")]
        IN = 15,
        [EnumMember]
        [Description("Iowa")]
        IA = 16,
        [EnumMember]
        [Description("Kansas")]
        KS = 17,
        [EnumMember]
        [Description("Kentucky")]
        KY = 18,
        [EnumMember]
        [Description("Louisiana")]
        LA = 19,
        [EnumMember]
        [Description("Maine")]
        ME = 20,
        [EnumMember]
        [Description("Maryland")]
        MD = 21,
        [EnumMember]
        [Description("Massachusetts")]
        MA = 22,
        [EnumMember]
        [Description("Michigan")]
        MI = 23,
        [EnumMember]
        [Description("Minnesota")]
        MN = 24,
        [EnumMember]
        [Description("Mississippi")]
        MS = 25,
        [EnumMember]
        [Description("Missouri")]
        MO = 26,
        [EnumMember]
        [Description("Montana")]
        MT = 27,
        [EnumMember]
        [Description("Nebraska")]
        NE = 28,
        [EnumMember]
        [Description("Nevada")]
        NV = 29,
        [EnumMember]
        [Description("New Hampshire")]
        NH = 30,
        [EnumMember]
        [Description("New Jersey")]
        NJ = 31,
        [EnumMember]
        [Description("New Mexico")]
        NM = 32,
        [EnumMember]
        [Description("New York")]
        NY = 33,
        [EnumMember]
        [Description("North Carolina")]
        NC = 34,
        [EnumMember]
        [Description("North Dakota")]
        ND = 35,
        [EnumMember]
        [Description("Ohio")]
        OH = 36,
        [EnumMember]
        [Description("Oklahoma")]
        OK = 37,
        [EnumMember]
        [Description("Oregon")]
        OR = 38,
        [EnumMember]
        [Description("Pennsylvania")]
        PA = 39,
        [EnumMember]
        [Description("Rhode Island")]
        RI = 40,
        [EnumMember]
        [Description("South Carolina")]
        SC = 41,
        [EnumMember]
        [Description("South Dakota")]
        SD = 42,
        [EnumMember]
        [Description("Tennessee")]
        TN = 43,
        [EnumMember]
        [Description("Texas")]
        TX = 44,
        [EnumMember]
        [Description("Utah")]
        UT = 45,
        [EnumMember]
        [Description("Vermont")]
        VT = 46,
        [EnumMember]
        [Description("Virginia")]
        VA = 47,
        [EnumMember]
        [Description("Washington")]
        WA = 48,
        [EnumMember]
        [Description("West Virginia")]
        WV = 49,
        [EnumMember]
        [Description("Wisconsin")]
        WI = 50,
        [EnumMember]
        [Description("Wyoming")]
        WY = 51
    }
    public enum Gender
    {
        [EnumMember]
        [Description("")]
        Unknown = 0,
        [EnumMember]
        [Description("Male")]
        Male = 1,
        [EnumMember]
        [Description("Female")]
        Female = 2
    }
}
