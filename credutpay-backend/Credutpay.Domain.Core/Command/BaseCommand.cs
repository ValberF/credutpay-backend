using MediatR;
using System.Text.Json.Serialization;


namespace Credutpay.Domain.Core.Command
{
    public abstract class BaseCommand : INotification
    {
        protected BaseCommand() 
        {
            Timestamp = DateTime.Now;
            MessageType = GetType().Name;
        }

        [JsonIgnore]
        public string MessageType { get; private set; }

        [JsonIgnore]
        public DateTime Timestamp { get; private set; }
    }
}
