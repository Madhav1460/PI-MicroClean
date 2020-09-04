using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ApiRisponse.ResponseUtil
{
    public enum ErrorMessageEnum
    {
        Email = 101,
        Phone = 102,
        Name = 103,
        Address = 104,
        WorngOTP = 105,
        PhoneOrEmailNotRegistor = 106,
        CartPartnerMatched = 110,
        VersionUpdate = 111,
        CODOptionClosed = 112
    }
}
