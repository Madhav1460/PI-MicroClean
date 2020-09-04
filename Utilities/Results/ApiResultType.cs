using System.Runtime.Serialization;

namespace Utilities.Results
{
    [DataContract(Namespace = "")]
    public enum ApiResultType
    {
        [EnumMember(Value = "Success")]
        Success,

        [EnumMember(Value = "Error")]
        Error,

        [EnumMember(Value = "ExceptionDuringOpration")]
        ExceptionDuringOpration
    }
}
