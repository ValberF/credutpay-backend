using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Core.Command;
using Credutpay.Domain.Registrations.Commands.Wallet;
using Credutpay.Domain.Registrations.Entities;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Domain.Registrations.Repositories.Read;
using MediatR;

namespace Credutpay.Domain.Registrations.CommandHandlers
{
    public class WalletCommandHandler : BaseCommandHandler,
        INotificationHandler<AddFundsCommand>,
        INotificationHandler<GetBalanceCommand>
    {
        private readonly IWalletReadRepository _walletReadRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletCommandHandler(
            IMediatorHandler bus,
            IWalletReadRepository walletReadRepository,
            IWalletRepository walletRepository) : base(bus)
        {
            _walletReadRepository = walletReadRepository;
            _walletRepository = walletRepository;
        }

        public Task Handle(AddFundsCommand notification, CancellationToken cancellationToken)
        {
            var wallet = _walletRepository.GetById(notification.WalletId);

            if (NotifyErrorDomainValidation(wallet == null, "Wallet not found"))
                return Task.CompletedTask;

            if (NotifyErrorDomainValidation(notification.Amount <= 0, "Amount must be greater than zero"))
                return Task.CompletedTask;

            try
            {
                wallet.Update(notification);
                _walletRepository.AddOrUpdate(wallet);
                return Task.CompletedTask;
            }
            catch (InvalidOperationException ex)
            {
                NotifyErrorDomainValidation(true, ex.Message);
                return Task.CompletedTask;
            }
        }

        public Task Handle(GetBalanceCommand notification, CancellationToken cancellationToken)
        {
            var wallet = _walletRepository.GetById(notification.WalletId);

            if (NotifyErrorDomainValidation(wallet == null, "Wallet not found"))
                return Task.CompletedTask;

            return Task.CompletedTask;
        }
    }
}