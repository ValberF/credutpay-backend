using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credutpay.Infra.Core.API.Response
{
    public class BaseEnvelopeResponse<T>
    {
        protected internal BaseEnvelopeResponse(T result, string errorMessage = "")
        {
            Result = result;
            ErrorMessage = errorMessage;
            OccuredIn = DateTime.Now;
        }

        public T Result { get; }
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; }
        public DateTime OccuredIn { get; }
    }
}
