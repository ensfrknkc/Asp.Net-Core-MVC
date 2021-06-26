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

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApsisDbContext>();

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IFlatService, FlatService>();
            services.AddScoped<IFlatRepository, FlatRepository>();

            var mappingConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile(new FlatProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
