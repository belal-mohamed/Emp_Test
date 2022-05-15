using Employees;
using Employees.src.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using Respawn.Graph;
using System.IO;
using System.Threading.Tasks;

namespace Application.IntegrationTest
{
    [SetUpFixture]
    public class Testing
    {

        private static IConfiguration _configuration = null!;
        private static IServiceScopeFactory _scopeFactory = null!;
        private static Checkpoint _checkpoint = null!;

        //private static string? _currentUserId;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

            _configuration = builder.Build();


            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Employees"));

            //services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            //var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            //    d.ServiceType == typeof(ICurrentUserService));

            //if (currentUserServiceDescriptor != null)
            //{
            //    services.Remove(currentUserServiceDescriptor);
            //}

            //// Register testing version
            //services.AddTransient(provider =>
            //    Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

            _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Table[] { "__EFMigrationsHistory" }

            };

            EnsureDatabase();
        }


        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
    where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }


        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        }

        public static async Task<TEntity> FindAsync<TEntity>(int id)
    where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(id);
        }
    }
}