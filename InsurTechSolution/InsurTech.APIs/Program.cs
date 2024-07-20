using InsurTech.Core.Entities;
using InsurTech.Core.Repositories;
using InsurTech.Core;
using InsurTech.Repository.Data;
using InsurTech.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using InsurTech.APIs.Errors;
using InsurTech.APIs.Middlewares;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using InsurTech.Core.Service;
using InsurTech.Service;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using InsurTech.Service.Models;
using CloudinaryDotNet;
using InsurTech.APIs.Helpers;
using Microsoft.AspNetCore.SignalR;


namespace InsurTech.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(op =>
			{
				op.SwaggerDoc("v1", new OpenApiInfo { Title = "InsurTech API", Version = "v1" });

				op.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme.",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});

				op.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
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
			});

			builder.Services.AddDbContext<InsurtechContext>(options =>
			{
				options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(options => { })
				.AddEntityFrameworkStores<InsurtechContext>()
				.AddDefaultTokenProviders();

			#region CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin", policyBuilder =>
				{
					policyBuilder.WithOrigins("http://localhost:4200")
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();
				});
			});
			#endregion

			#region Authentication and JWT
			// Reset Password
			builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(10));

			// Login By Google + JWT
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
                // Add this to support SignalR authentication
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our SignalR hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/api/notificationHub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
           
        })
			.AddGoogle(option =>
			{
				option.ClientId = "30498991812-uog175jdj3vb9foj41sv9g2l88teu11n.apps.googleusercontent.com";
				option.ClientSecret = "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";
			});


            #endregion

			builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
       builder.Services.AddSignalR();

       builder.Services.AddAuthorization();
      
       #region Cloudinary - UploadService

			builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

			builder.Services.AddScoped<IUploadService, UploadService>();

			#endregion


     builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IEmailService, EmailService>();
			builder.Services.AddScoped<IRequestService, RequestService>();
            // add CardValidationService to the DI container
            builder.Services.AddHttpClient();




            #region Validation Error Handling
            builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState
						.Where(p => p.Value.Errors.Count > 0)
						.SelectMany(p => p.Value.Errors)
						.Select(e => e.ErrorMessage)
						.ToArray();
					var validationErrorResponse = new ApiValidationErrorResponse
					{
						Errors = errors
					};
					return new BadRequestObjectResult(validationErrorResponse);
				};
			});
			#endregion

			#region Mapper
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			#endregion

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			app.UseMiddleware<ExceptionMiddleWare>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")),
				RequestPath = ""
			});

			app.UseStatusCodePagesWithRedirects("/error/{0}");

			app.UseHttpsRedirection();

			app.UseCors("AllowSpecificOrigin");

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapHub<NotificationHub>("/api/notificationHub");
			app.Run();
		}
	}
}

