using System.Runtime.Serialization;

namespace Utilities.Results
{
    [DataContract(Namespace = "")]
    public enum ApiResultMessage
    {
        [EnumMember(Value = "Success")]
        SuccessMessage,

        [EnumMember(Value = "Error")]
        ErrorMessage,

        [EnumMember(Value = "ExceptionDuringOpration")]
        ExceptionMessage
    }
}
