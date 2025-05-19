using AccessControlSystem.Application.External;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace AccessControlSystem.Infrastructure.Messaging;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;
    private readonly string _topic;

    public KafkaProducer(IProducer<Null, string> producer, IOptions<KafkaSettings> kafkaSettings, ILogger<KafkaProducer> logger)
    {
        _producer = producer;
        _topic = kafkaSettings.Value.Topic;
        _logger = logger;
    }

    public async Task SendMessageAsync(object payload)
    {
        var messageJson = JsonSerializer.Serialize(payload);

        try
        {
            var result = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = messageJson });
            _logger.LogInformation("Kafka message sent to topic {Topic}: {Message}", _topic, messageJson);
        }
        catch (ProduceException<Null, string> ex)
        {
            _logger.LogError(ex, "Kafka message failed: {Message}", messageJson);
        }
    }
}
