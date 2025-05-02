using MediatR;
using Credutpay.Domain.Core.Bus;
using Credutpay.Domain.Registrations.Repositories;
using Credutpay.Infra.Core.Bus;
using Credutpay.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Credutpay.Domain.Core.Notification;
using Credutpay.Domain.Registrations.CommandHandlers;
using Credutpay.Domain.Registrations.Repositories.Read;
using Credutpay.Domain.Registrations.Commands.User;
using Credutpay.Domain.Registrations.Commands.Login;
using Credutpay.Domain.Core.Auth;
using Credutpay.Infra.Core.Auth;
using Credutpay.Domain.Registrations.Commands.Wallet;
using Credutpay.Domain.Registrations.Commands.WalletTransaction;

namespace Credutpay.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            ConfigureDbContext(services, configuration);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            RegisterCommands(services);
            RegisterDomainEvents(services);
            RegisterRepositories(services);
            RegisterDomainServices(services);
        }

        private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        static void RegisterCommands(IServiceCollection services) 
        {

            services.AddScoped<INotificationHandler<CreateUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<DeleteUserCommand>, UserCommandHandler>();

            services.AddScoped<INotificationHandler<AddFundsCommand>, WalletCommandHandler>();
            services.AddScoped<INotificationHandler<GetBalanceCommand>, WalletCommandHandler>();

            services.AddScoped<INotificationHandler<CreateTransactionCommand>, WalletTransactionCommandHandler>();


            services.AddScoped<INotificationHandler<UserLoginCommand>, LoginCommandHandler>();
        }

        static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserReadRepository, Data.Repositories.PostgreSQL.UserRepository>();
            services.AddScoped<IUserRepository, Data.Repositories.PostgreSQL.UserRepository>();
            services.AddScoped<IWalletReadRepository, Data.Repositories.PostgreSQL.WalletRepository>();
            services.AddScoped<IWalletRepository, Data.Repositories.PostgreSQL.WalletRepository>();
            services.AddScoped<IWalletTransactionRepository, Data.Repositories.PostgreSQL.WalletTransactionRepository>();
        }

        static void RegisterDomainEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
