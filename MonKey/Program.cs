namespace MonKey.Web
{
    using Data.Models;
    using Data.Repository;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MonKey.Data;
    using Services.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder
                                          .Configuration
                                          .GetConnectionString("DefaultConnection") ??
                                      throw new InvalidOperationException(
                                          "Connection string 'DefaultConnection' not found.");
            
            //TODO: AutoMapperConfig.RegisterMappings();

            builder.Services.AddDbContext<MonkeyDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.RegisterRepositories(typeof(AccountRepository));
            builder.Services.RegisterUserServices(typeof(AccountService));

            //TODO: Add extensions in Monkey.Web.Infrastructure
            //builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            //TODO: Slug
            //builder.Services.AddSingleton<ISlugGenerator, SlugGenerator>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<MonkeyDbContext>();

            // TODO: Add CORS Policy
            // builder.Services.AddCors(config =>
            // {
            //     public const string MvcDomainPolicyName = "AllowMvcDomain"; in ApplicationConstants.cs in Globally Common Project
            //     config.AddPolicy(MvcDomainPolicyName, corsPolicyBuilder =>
            //     {
            //         corsPolicyBuilder
            //             .WithOrigins("https://localhost:7180")
            //             .WithMethods("GET", "POST")
            //             .AllowAnyHeader();
            //     });
            // });

            builder.Services.AddControllersWithViews();

            // TODO: Add AutoMapper
            // builder.Services.AddSingleton(AutoMapperConfig.MapperInstance);

            builder.Services.Configure<IdentityOptions>(options =>
            {
                ConfigureIdentityOptions(builder.Configuration, options);
            });
            
            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("Permissions-Policy", "unload=*");
                await next();
            });

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            
            //TODO: app.UseCors(MvcDomainPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();

            //TODO: Add extensions in Monkey.Web.Infrastructure
            //app.UseRoleSeeder();
            //app.UseAdminUserSeeder();

            app.MapStaticAssets();

            //TODO: Add controller routes for different areas and slug route too
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.MapRazorPages()
                .WithStaticAssets();

            app.Run();
        }

        private static void ConfigureIdentityOptions(ConfigurationManager configuration, IdentityOptions options)
        {
            // Password settings
            options.Password.RequireDigit = configuration.GetValue<bool>("Identity:Password:RequireDigit");
            options.Password.RequireLowercase = configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequiredLength = configuration.GetValue<int>("Identity:Password:RequiredLength");
            options.Password.RequiredUniqueChars = configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");

            //  Sign-in settings
            options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            options.SignIn.RequireConfirmedPhoneNumber = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");
            options.SignIn.RequireConfirmedAccount = configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = configuration.GetValue<TimeSpan>("Identity:Lockout:DefaultLockoutTimeSpan");
            options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("Identity:Lockout:MaxFailedAccessAttempts");
            options.Lockout.AllowedForNewUsers = configuration.GetValue<bool>("Identity:Lockout:AllowedForNewUsers");

            // User settings
            options.User.RequireUniqueEmail = configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}