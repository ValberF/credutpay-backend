using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credutpay.Infra.Core.API.Response
{
    public class EnvelopeResponse : BaseEnvelopeResponse<string>
    {
        public EnvelopeResponse(string errorMessage = "")
            : base(string.Empty, errorMessage)
        {
        }

        public static BaseEnvelopeResponse<T> Success<T>(T result)
            => new BaseEnvelopeResponse<T>(result);

        public static EnvelopeResponse Success()
            => new EnvelopeResponse();

        public static EnvelopeResponse Error(string errorMessage)
            => new EnvelopeResponse(errorMessage);
    }
}
