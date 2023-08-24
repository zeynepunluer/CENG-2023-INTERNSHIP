using jsonProj.DTOs;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using jsonProj.Health;

namespace jsonProj { 
    public class Startup
    {
        private readonly IConfiguration configuration;
        
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IMongoClient>(s =>
            {
                var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            services.AddHealthChecks().AddCheck<MongoDBHealthCheck>("MongoDBHealthCheck");

            services.AddControllers();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");

            });


        }
        
        


    }
}
