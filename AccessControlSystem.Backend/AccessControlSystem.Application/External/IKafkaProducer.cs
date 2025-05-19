namespace AccessControlSystem.Application.External;

public interface IKafkaProducer
{
    Task SendMessageAsync(object message);
}
