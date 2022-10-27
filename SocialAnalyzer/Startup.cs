using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialAnalyzer.Configurations;
using SocialAnalyzer.SDK.Options;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SocialAnalyzer.SDK.Options;
using SocialAnalyzer.DAL.Models;
using SocialAnalyzer.Services;
using System.IO;

namespace SocialAnalyzer
{
    public class Startup
    {
        private readonly JWTOptions _jwtOptions = new JWTOptions();
        private readonly AppOptions _appOptions = new AppOptions();
        private readonly AppOptions _messaggeOptions = new AppOptions();



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection(JWTOptions.SectionName).Bind(_jwtOptions);
            Configuration.GetSection(AppOptions.SectionName).Bind(_appOptions);
            Configuration.GetSection(MessageOptions.SectionName).Bind(_messaggeOptions);

        }
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(_appOptions.UrlWeb)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            string mysqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<analizerContext>(options => options.UseMySQL(mysqlConnectionStr));

            services
               .AddMvcCore()
               .AddNewtonsoftJson()
               .AddDataAnnotations();

            //services.AddAuthorizationCore(options =>
            //{
            //    options.AddPolicy("Supervisor", policy =>
            //      policy.RequireClaim("Rol", "Supervisor"));

            //    options.AddPolicy("SuperAdmin", policy =>
            //     policy.RequireClaim("Rol", "SuperAdmin"));


            //    options.AddPolicy("Supervisor_o_SuperAdmin", policy =>
            //      policy.RequireAssertion(context =>
            //                        context.User.HasClaim(c =>
            //                           (c.Value == "Supervisor" ||
            //                            c.Value == "SuperAdmin")
            //                            )));
            //});

          
           

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _jwtOptions.Issuer,
                        ValidAudience = _jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key))
                    };
                });

            AddDependences(services);
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.ConfigureServiceDependencies();

            services.AddOptions();
            services.Configure<JWTOptions>(Configuration.GetSection(JWTOptions.SectionName));
            services.Configure<AppOptions>(Configuration.GetSection(AppOptions.SectionName));
            services.Configure<MessageOptions>(Configuration.GetSection(MessageOptions.SectionName));

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Analyzer API", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingresar el JWT con Bearer en el campo. Ej: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
                }
              });
                var filePath = Path.Combine(AppContext.BaseDirectory, "SocialAnalyzer.xml");
                c.IncludeXmlComments(filePath);
            });
            
            //Ignora el loop 
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialAnalyzer v1"));
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDependences(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
