using System.Runtime.Serialization;

namespace Utilities.Results
{
    [DataContract(Namespace = "")]
    public class ApiResultCode
    {
        private int _errorCode = 0;
        private string _messageText = string.Empty;

        /// <summary>
        /// Creates a default cunstructor with Error ApiResultType
        /// </summary>
        public ApiResultCode()
        {
            this.ResultType = ApiResultType.Error;
        }

        /// <summary>
        /// Creates a cunstructor with result type error code and message text
        /// </summary>
        /// <param name="resultType">Api Result Type as Success or Error</param>
        /// <param name="errorCode">Error code if any error comes</param>
        /// <param name="messageText">message text if any error exists</param>
        public ApiResultCode(ApiResultType resultType, int errorCode = 0, string messageText = "")
        {
            this.ResultType = resultType;
            this.ErrorCode = errorCode;
            if (!string.IsNullOrEmpty(messageText))
            {
                this.MessageText = messageText;
            }
        }

        [DataMember]
        public ApiResultType ResultType { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            private set
            {
                _errorCode = value;
            }
        }

        [DataMember(EmitDefaultValue = false)]
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            private set
            {
                _messageText = value;
            }
        }
    }
}
