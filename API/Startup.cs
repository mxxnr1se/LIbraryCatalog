using API.Extensions;
using BLL.Services.Interfaces;
using BLL.Services.Realizations;
using DAL.Contexts;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace API
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
            services.AddDbContext<LibraryContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(true);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReaderService, ReaderService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IGenreService, GenreService>();

            services.SwaggerConfigure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger(c => {c.SerializeAsV2 = true;});
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryAPI");
                c.RoutePrefix = string.Empty;
            });
            app.ConfigureExceptionHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}