using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace jsonProj.Health
{
    public class MongoDBHealthCheck : IHealthCheck
    {
        private readonly IMongoClient mongoClient;

        public MongoDBHealthCheck(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;

        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = await mongoClient.ListDatabaseNames().ToListAsync();

                if (database.Any())
                {
                    return HealthCheckResult.Healthy();
                }
                else
                {
                    return HealthCheckResult.Unhealthy("MongoDB connection failed.");

                }
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy("MongoDB health cannot be checked: " + ex.Message);

            }
        }
    }
}
