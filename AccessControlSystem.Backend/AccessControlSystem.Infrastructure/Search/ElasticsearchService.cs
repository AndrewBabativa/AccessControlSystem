using AccessControlSystem.Application.DTOs;
using AccessControlSystem.Application.External;
using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;

namespace AccessControlSystem.Infrastructure.Search;

public class ElasticsearchService : IElasticsearchService
{
    private readonly IElasticClient _elasticClient;
    private readonly ILogger<ElasticsearchService> _logger;
    private readonly string _indexName;

    public ElasticsearchService(IElasticClient elasticClient, IOptions<ElasticSettings> settings, ILogger<ElasticsearchService> logger)
    {
        if (settings?.Value == null || string.IsNullOrWhiteSpace(settings.Value.DefaultIndex))
            throw new ArgumentNullException(nameof(settings), "Elasticsearch configuration is missing or invalid.");

        _elasticClient = elasticClient;
        _indexName = settings.Value.DefaultIndex;
        _logger = logger;
    }

    public async Task IndexPermissionAsync(PermissionDto permission)
    {
        try
        {
            var pingResponse = await _elasticClient.PingAsync();
            if (pingResponse.IsValid)
            {
                Console.WriteLine("Elasticsearch está conectado");
            }
            else
            {
                Console.WriteLine("Error conectando a Elasticsearch: " + pingResponse.OriginalException?.Message);
            }

            var response = await _elasticClient.IndexAsync(permission, idx => idx.Index(_indexName));
            if (!response.IsValid)
            {
                _logger.LogError("Failed to index permission in Elasticsearch: {Reason}", response.ServerError?.Error?.Reason);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while indexing permission.");
        }
    }

}
