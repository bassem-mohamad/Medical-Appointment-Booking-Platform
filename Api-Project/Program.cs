using System.Text;
using Api_Project.Context;
using Api_Project.Models;
using Api_Project.Repository;
using Api_Project.Services;
using Api_Project.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<UnitWork>();
            builder.Services.AddScoped(typeof(GenericRepository<Location>));
            builder.Services.AddScoped(typeof(GenericRepository<Specialty>));
            builder.Services.AddScoped(typeof(GenericRepository<Doctor>));
            builder.Services.AddScoped(typeof(GenericRepository<Patient>));
            builder.Services.AddScoped(typeof(GenericRepository<Appointment>));
            builder.Services.AddScoped(typeof(GenericRepository<DoctorSchedule>));
            builder.Services.AddScoped(typeof(GenericRepository<Review>));
            builder.Services.AddScoped(typeof(GenericRepository<TimeSlot>));

            // Register Services
            builder.Services.AddScoped<LocationService>();
            builder.Services.AddScoped<SpecialtyService>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<DoctorScheduleService>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<TimeSlotService>();

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApiDbContext>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Medical Appointment Booking API",
                    Version = "v1",
                    Description = "API with JWT Authentication"
                });

                // Add JWT Authentication to Swagger
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    //Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and then your token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["jwt:issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["jwt:audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(op =>
                {
                    op.SwaggerEndpoint("/swagger/v1/swagger.json", "Medical API V1");
                });
            }

            app.UseCors(op =>
            {
                op.AllowAnyOrigin();
                op.AllowAnyHeader();
                op.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
