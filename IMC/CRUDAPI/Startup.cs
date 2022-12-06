using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Models; //Contexto.cs

namespace CRUDAPI
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
              // opcao de congigurar banco de dados em memoria para testes direto no startup
           // services.AddDbContext<Contexto>( options => options.UseSqlite("DataSource=folhas.db;Cache=shared"));

           // opcao de congigurar banco usando o arquivo appsettings.json
           services.AddDbContext<Contexto>( options => options.UseSqlite(Configuration.GetConnectionString("ConexaoBD")));

           services.AddCors(); //habilita o cors para acesso de outros dominios a api 


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUDAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUDAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //AllowAnyOrigin = permite qualquer origem
            //AllowAnyMethod = permite qualquer metodo
            //AllowAnyHeader = permite qualquer cabecalho
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //habilita o cors para acesso de outros dominios a api
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
