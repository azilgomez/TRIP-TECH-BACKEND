using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using TRIP.Platform.Service.API.Middleware;
using TRIP.Platform.Service.Core.Features.Users;
using TRIP.Platform.Service.Infrastructure.DBContext;
namespace TRIP.Platform.Service.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			CurrentEnvironment = env;
		}

		public IConfiguration Configuration { get; }
		private IWebHostEnvironment CurrentEnvironment { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddMediatR(typeof(GetUsersQueryHandler).Assembly);			

			// Add cross-origin resource sharing (CORS).
			services.AddCors((options =>
			{
				options.AddPolicy("FrontEnd", builder => builder
			   .WithOrigins("https://dev-sredplatform.ey.com", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials")
			   .WithOrigins("http://localhost:4200", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials")
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .AllowCredentials()
			   );
			}));

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.MinimumSameSitePolicy = SameSiteMode.Strict; // Enforce SameSite policy
				options.Secure = CookieSecurePolicy.Always;          // Enforce Secure cookies
				options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always; // Enforce HttpOnly
			});

			services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});
			
			services.AddCors();
			services.AddHttpContextAccessor();

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.AddControllers();
			services.AddMvc();
			services.AddSignalR();

			services.AddMemoryCache();		

			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			});					

			services.Configure<FormOptions>(x =>
			{
				x.ValueLengthLimit = int.MaxValue;
				x.MultipartBodyLengthLimit = int.MaxValue;
			});

			//services.AddScoped<AntiForgeryTokenAttribute>();
			services.AddAntiforgery(options =>
			{
				options.HeaderName = "X-XSRF-TOKEN";
				options.SuppressXFrameOptionsHeader = true;
			});
			
			services.AddDbContext<TripDbContext>(options => options.UseSqlServer(
											 Configuration.GetConnectionString("DefaultConnection"),
											 sqlServerOptions => sqlServerOptions.CommandTimeout(1800)));
			

			if (!CurrentEnvironment.IsProduction())
			{
				services.AddSwaggerGen(
				options =>
				{
					var containerBuilder = new ContainerBuilder();
					containerBuilder.Populate(services);
					
					var xmlpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml");
					if (File.Exists(xmlpath))
					{
						options.IncludeXmlComments(xmlpath);
					}
				});
			}
			var builder = new ContainerBuilder();
			builder.Populate(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
		{
			//app.UseIpRateLimiting();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else { app.UseHsts(); }
			//app.UseMiddleware<ExceptionMiddleware>();
			var cspString = "default-src 'self';" + (env.IsDevelopment() == true ? "style-src 'self' 'unsafe-inline';script-src https://stackpath.bootstrapcdn.com/ 'self' 'unsafe-inline';" : string.Empty);
			app.Use(async (context, next) =>
			{
				context.Response.Headers.Add("Content-Security-Policy", cspString);
				await next();
			});
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseCors("FrontEnd");
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseRouting();

			app.UseAuthorization();
			app.UseHttpsRedirection();			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();				
			});
			if (!env.IsProduction())
			{
				app.UseSwagger();
				app.UseSwaggerUI();				
			}
			app.UseMvc();
		}		
		public void ConfigureContainer(ContainerBuilder builder) => builder.RegisterModule(new AutofacModule());
	}
}