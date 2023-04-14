using elk_demo.Models;
using Nest;

namespace elk_demo.Common;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["ELKConfig:Uri"]!;
        var defaultIndex = configuration["ELKConfig:index"]!;
        var settings = new ConnectionSettings(new Uri(url))
                            .PrettyJson()
                            .DefaultIndex(defaultIndex);

        AddDefaultMapping(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex);
    }

    private static void AddDefaultMapping(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Product>(
            p => p.Ignore(x => x.Price)
                  .Ignore(x => x.Id)
                  .Ignore(x => x.Quantity)
        );
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, i => i.Map<Product>(x => x.AutoMap()));
    }

}