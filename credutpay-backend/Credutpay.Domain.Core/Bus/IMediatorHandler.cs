using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credutpay.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendAsync<T>(T command) where T : BaseCommand;
        Task RaiseAsync<T>(T command) where T : BaseEvent;
    }
}
