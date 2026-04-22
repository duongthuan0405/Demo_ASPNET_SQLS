
using Microsoft.AspNetCore.Mvc;

namespace Test
{
    public class TestClass
    {
        public interface IService
        {
            
        }
        public class ServiceImplementation : IService
        {
            
        }

        public class CustomMiddleware : IMiddleware
        {
            public Task InvokeAsync(HttpContext context, RequestDelegate next)
            {
                throw new NotImplementedException();
            }
        }

        public void TestFunction()
        {
            Microsoft.Extensions.DependencyInjection.IServiceCollection services = new ServiceCollection();
            services.AddScoped<IService, ServiceImplementation>();
            services.AddTransient<IService, ServiceImplementation>();
            services.AddSingleton<IService, ServiceImplementation>();

            var builder = WebApplication.CreateBuilder();
            // DI, Config application, ...
            var app = builder.Build();
            
            app.UseAuthentication();
            app.UseAuthentication();
            app.UseMiddleware<CustomMiddleware>();
            
        }

        public class TestController : Controller
        {
            private IService _service;
            public TestController(IService service)
            {
                _service = service;
            }
        }
    }
}