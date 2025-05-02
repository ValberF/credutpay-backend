using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Registrations.Commands.Wallet;
using Credutpay.Domain.Registrations.Commands.WalletTransaction;
using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Domain.Registrations.Repositories.Read;
using MediatR;

namespace Credutpay.Domain.Registrations.CommandHandlers
{
    public class WalletTransactionCommandHandler : BaseCommandHandler,
        INotificationHandler<CreateTransactionCommand>
    {
        private readonly IWalletTransactionRepository _walletTransactionRepository;
        private readonly IWalletReadRepository _walletReadRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletTransactionCommandHandler(
            IMediatorHandler bus,
            IWalletTransactionRepository walletTransactionRepository,
            IWalletReadRepository walletReadRepository,
            IWalletRepository walletRepository) : base(bus)
        {
            _walletTransactionRepository = walletTransactionRepository;
            _walletReadRepository = walletReadRepository;
            _walletRepository = walletRepository;
        }

        public Task Handle(CreateTransactionCommand notification, CancellationToken cancellationToken)
        {
            var walletReceiver = _walletRepository.GetById(notification.WalletReceiverId);
            var walletSender = _walletRepository.GetById(notification.WalletSenderId);

            if (NotifyErrorDomainValidation(walletReceiver == null, "Wallet not found") 
                || NotifyErrorDomainValidation(notification.Amount <= 0, "Amount must be greater than zero") 
                || NotifyErrorDomainValidation((walletSender.Amount < notification.Amount), "Unavailable amount")) 
                return Task.CompletedTask;

            try
            {
                var walletTransaction = new WalletTransaction(notification);
                walletReceiver.AddAmount(notification.Amount, walletTransaction);
                walletSender.DeductAmount(notification.Amount, walletTransaction);

                _walletTransactionRepository.AddOrUpdate(walletTransaction);

                _walletRepository.AddOrUpdate(walletReceiver);
                _walletRepository.AddOrUpdate(walletSender);
                return Task.CompletedTask;
            }
            catch (InvalidOperationException ex)
            {
                NotifyErrorDomainValidation(true, ex.Message);
                return Task.CompletedTask;
            }
        }
    }
}
