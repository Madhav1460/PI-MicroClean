using System.Runtime.Serialization;

namespace Utilities.Results
{
    public class ApiResult<T>
    {
        /// <summary>
        /// Create a default cunstructor which provides a new instance of Api Result Code with Error Code type
        /// </summary>
        public ApiResult()
        {
            ResultCode = new ApiResultCode();
        }

        /// <summary>
        /// Create ApiResult with Api Result Code and Object
        /// </summary>
        /// <param name="apiResultCode">Api Result code with status</param>
        /// <param name="userObject">Custom User Object to be return</param>
        public ApiResult(ApiResultCode apiResultCode, T userObject)
            : this(apiResultCode)
        {
            this.UserObject = userObject;
        }

        /// <summary>
        /// Create ApiResult with Api Result Code
        /// </summary>
        /// <param name="apiResultCode">Api Result code with status</param>
        public ApiResult(ApiResultCode apiResultCode)
        {
            this.ResultCode = apiResultCode;
        }

        [DataMember]
        public ApiResultCode ResultCode { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        public T UserObject { get; private set; }

        /// <summary>
        /// Get True if ApiResult has success result type and UserObject doesn't contains NULL value
        /// </summary>
        public bool HasSuccess
        {
            get
            {
                return (this.ResultCode.ResultType == ApiResultType.Success && this.UserObject != null);
            }
        }
    }
}
