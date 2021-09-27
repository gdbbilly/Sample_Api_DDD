using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleDDD.Domain.Entities;
using SampleDDD.Domain.Interfaces;
using SampleDDD.Infra.Data.Context;
using SampleDDD.Infra.Data.Repository;
using SampleDDD.Service.Services;
using SampleDDDApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDDDApp
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
            services.AddControllers();
            
            //ADD DB CONTEXT
            services.AddDbContext<SqlContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //REPOSITORIES
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            
            //SERVICES
            services.AddScoped<IBaseService<User>, BaseService<User>>();

            //AUTOMAPPER
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserModel, User>();
                config.CreateMap<UpdateUserModel, User>();
                config.CreateMap<User, UserModel>();
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
