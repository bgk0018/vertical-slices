﻿using System;
using System.Net;
using Banking.Accounts.Domain;
using Banking.Accounts.Exceptions;
using Banking.Accounts.Mappers;
using Banking.Accounts.Models;
using Banking.Accounts.Services;
using GlobalExceptionHandler.WebApi;
using Highway.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Random.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace Banking.Accounts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var uow = new InMemoryDataContext();
            var repository = new Repository(uow);

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Accounts API", Version = "v1" });
                })
                .AddSingleton<IEventBus, EventBusStub>()
                .AddScoped<IMapper<Account, AccountModel>, AccountMapper>()
                .AddScoped<IAccountFactory, AccountFactory>()
                .AddSingleton<IRepository>(repository)
                .AddScoped<AccountsService>()
                .AddScoped<SimpleIdFactory>()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/error")
                .WithConventions(config =>
            {
                config.ContentType = "application/json";
                config.MessageFormatter(s => JsonConvert.SerializeObject(new
                {
                    Message = "An error occurred whilst processing your request"
                }));

                config.ForException<BadRequestException>()
                    .ReturnStatusCode(400)
                    .UsingMessageFormatter((ex, context) => JsonConvert.SerializeObject(new
                    {
                        ex.Message
                    }));

                config.ForException<NotFoundException>()
                    .ReturnStatusCode(404)
                    .UsingMessageFormatter((ex, context) => JsonConvert.SerializeObject(new
                    {
                        ex.Message
                    }));

            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounts API V1");
            });
            app.UseMvc();
        }
    }
}
