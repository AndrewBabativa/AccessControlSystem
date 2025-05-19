namespace AccessControlSystem.Infrastructure.Messaging;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string Topic { get; set; } = "permissions-events";
}
