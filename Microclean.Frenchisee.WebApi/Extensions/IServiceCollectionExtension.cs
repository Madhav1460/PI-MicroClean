using AutoMapper;
using Microclean.DataAccessLayer.Core;
using Microclean.DataAccessLayer.Persistence;
using Microclean.DataModel;
using Microclean.ProviderLayer.Mapper;
using Microclean.ProviderLayer.Postal;
using Microclean.RepositoryLayer.IRepositories;
using Microclean.RepositoryLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Microclean.Frenchisee.WebApi.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCoreAppMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
        public static IServiceCollection AddProvider(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork<MicrocleanDbContext>, UnitOfWork<MicrocleanDbContext>>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IFranchiseeRepository, FranchiseeRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IUserTranninRepository, UserTranninRepository>();
            services.AddTransient<ITestForEndUserRepository, TestForEndUserRepository>();
            services.AddSingleton<IEmailSender, EmailSender>();
            return services;
        }
    }
}
