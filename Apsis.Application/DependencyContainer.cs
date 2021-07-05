using Apsis.Application.Dto;
using Apsis.Application.Interfaces;
using Apsis.Application.Profiles;
using Apsis.Application.Services;
using Apsis.Domain.Interfaces;
using Apsis.Domain.Models;
using Apsis.Infrastructure;
using Apsis.Infrastructure.Context;
using Apsis.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterApsis(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApsisDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"))
            .UseLazyLoadingProxies());

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApsisDbContext>();

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IFlatService, FlatService>();
            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHttpClient<ICreditCardService, CreditCardService>(options =>
            {
                options.BaseAddress = new Uri(configuration["CreditCard:Url"]);
            });

            var mappingConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile(new FlatProfile());
                cfg.AddProfile(new BillProfile());
                cfg.AddProfile(new BlockProfile());
                cfg.AddProfile(new MessageProfile());
                cfg.AddProfile(new SubscriptionProfile());
                cfg.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
